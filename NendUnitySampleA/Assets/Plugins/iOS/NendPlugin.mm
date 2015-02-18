//
//  NendPlugin.mm
//  Unity-iPhone
//
//  Created by ADN on 2013/11/06.
//
//

#include "iPhone_target_Prefix.pch"
#include "iPhone_OrientationSupport.h"

#import <objc/runtime.h>

#import "NADView.h"
#import "NADIconView.h"
#import "NADIconLoader.h"
#import "NADInterstitial.h"

static const char* INTERSTITIAL_GAME_OBJECT = "NendAdInterstitial";

extern UIView* UnityGetGLView();
extern UIViewController* UnityGetGLViewController();

static NSMutableDictionary* _bannerAdDict = [[NSMutableDictionary alloc] init];
static NSMutableDictionary* _iconAdDict = [[NSMutableDictionary alloc] init];

enum NendGravity
{
    LEFT = 1,
    TOP = 2,
    RIGHT = 4,
    BOTTOM = 8,
    CENTER_VERTICAL = 16,
    CENTER_HORIZONTAL = 32,
};

enum NendOrientation
{
    HORIZONTAL = 0,
    VERTICAL = 1,
    UNSPECIFIED = 2,
};

enum NendBannerSize
{
    SIZE_320X50 = 0,
    SIZE_320X100 = 1,
    SIZE_300X100 = 2,
    SIZE_300X250 = 3,
    SIZE_728X90 = 4,
};

NSString* CreateNSString(const char* string)
{
    if ( string )
        return @(string);
    else
        return @"";
}

char* MakeStringCopy(const char* string)
{
    if ( NULL == string )
        return NULL;
    
    char* res = (char *)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

CGSize BannerSize(NendBannerSize size)
{
    switch ( size )
    {
        case SIZE_320X50:
            return CGSizeMake(320, 50);
        case SIZE_320X100:
            return CGSizeMake(320, 100);
        case SIZE_300X100:
            return CGSizeMake(300, 100);
        case SIZE_300X250:
            return CGSizeMake(300, 250);
        case SIZE_728X90:
            return CGSizeMake(728, 90);
        default:
            return CGSizeZero;
    }
}

CGPoint CalculatePointFromGravityAndMargins(int gravity, CGSize viewSize, int left, int top, int right, int bottom)
{
    CGPoint point = CGPointZero;
    CGSize screenSize = UnityGetGLView().bounds.size;
    
    if ( 0 != (gravity & CENTER_HORIZONTAL) )
        point.x = (screenSize.width - viewSize.width) / 2;
    if ( 0 != (gravity & RIGHT) )
        point.x = screenSize.width - viewSize.width;
    if ( 0 != (gravity & LEFT) )
        point.x = 0.0f;
    
    if ( 0 != (gravity & CENTER_VERTICAL) )
        point.y = (screenSize.height - viewSize.height) / 2;
    if ( 0 != (gravity & BOTTOM) )
        point.y = screenSize.height - viewSize.height;
    if ( 0 != (gravity & TOP) )
        point.y = 0.0f;
    
    point.x += left;
    point.y += top;
    point.x -= right;
    point.y -= bottom;
    
    return point;
}

UIColor* CreateUIColorFromColorCode(NSString* colorCode)
{
    if ( !colorCode || 0 == colorCode.length )
        return [UIColor blackColor];
    
    if ( [[colorCode substringWithRange:NSMakeRange(0, 1)] isEqualToString:@"#"] )
        colorCode = [colorCode substringWithRange:NSMakeRange(1, colorCode.length - 1)];
    
    NSString *hexCodeStr;
    const char *hexCode;
    char *endptr;
    CGFloat red, green, blue;
    
    for ( NSInteger i = 0; i < 3; i++ )
    {
        hexCodeStr = [NSString stringWithFormat:@"+0x%@", [colorCode substringWithRange:NSMakeRange(i * 2, 2)]];
        hexCode    = [hexCodeStr cStringUsingEncoding:NSASCIIStringEncoding];
        
        switch (i)
        {
            case 0:
                red = strtol(hexCode, &endptr, 16);
                break;
            case 1:
                green = strtol(hexCode, &endptr, 16);
                break;
            case 2:
                blue = strtol(hexCode, &endptr, 16);
            default:
                break;
        }
    }
    
    return [UIColor colorWithRed:red / 255.0 green:green / 255.0 blue:blue / 255.0 alpha:1.0];
}

CGFloat IconActualHeight(CGFloat size, BOOL titleVisible, BOOL spaceEnabled)
{
    if ( titleVisible && !spaceEnabled )
        return size + size * 15 / NAD_ICON_SIZE_57x57.height;
    else
        return size;
}

NSInteger InterstitialStatusCodeToInteger(NADInterstitialStatusCode status)
{
    switch ( status )
    {
        case SUCCESS:
            return 0;
        case INVALID_RESPONSE_TYPE:
            return 1;
        case FAILED_AD_REQUEST:
            return 2;
        case FAILED_AD_DOWNLOAD:
            return 3;
        default:
            return -1;
    }
}

NSInteger InterstitialClickTypeToInteger(NADInterstitialClickType type)
{
    switch ( type )
    {
		case DOWNLOAD:
			return 0;
		case CLOSE:
			return 1;
		default:
			return -1;
    }
}

BOOL ShouldAutorotateIMP(id self, SEL _cmd)
{
    if ( [UnityGetGLViewController() respondsToSelector:@selector(shouldAutorotate)] )
        return [UnityGetGLViewController() shouldAutorotate];
    else
        return YES;
}

NSUInteger SupportedInterfaceOrientationsIMP(id self, SEL _cmd)
{
    if ( [UnityGetGLViewController() respondsToSelector:@selector(supportedInterfaceOrientations)] )
        return [UnityGetGLViewController() supportedInterfaceOrientations];
    else
        return UIInterfaceOrientationMaskAll;
}

BOOL ShouldAutorotateToInterfaceOrientationIMP(id self, SEL _cmd, UIInterfaceOrientation interfaceOrientation)
{
    if ( [UnityGetGLViewController() respondsToSelector:@selector(shouldAutorotateToInterfaceOrientation:)] )
        return [UnityGetGLViewController() shouldAutorotateToInterfaceOrientation:interfaceOrientation];
    else
        return YES;
}

void AddRotateMethodToInterstitialViewController()
{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        Class cls = NSClassFromString(@"NADInterstitialViewController");
        if ( _ios60orNewer )
        {
            class_replaceMethod(cls, @selector(shouldAutorotate), (IMP)&ShouldAutorotateIMP, "c8@0:4");
            class_replaceMethod(cls, @selector(supportedInterfaceOrientations), (IMP)&SupportedInterfaceOrientationsIMP, "I8@0:4");
        }
        else
            class_replaceMethod(cls, @selector(shouldAutorotateToInterfaceOrientation:), (IMP)&ShouldAutorotateToInterfaceOrientationIMP, "c12@0:4i8");
    });
}

///-----------------------------------------------
/// @name Interfaces
///-----------------------------------------------

@interface NADViewEventDispatcher : NSObject <NADViewDelegate>

@property (nonatomic, copy) NSString* gameObject;

- (instancetype) initWithGameObject:(NSString *)gameObject;

@end

//==============================================================================

@interface NADIconLoaderEventDispatcher : NSObject <NADIconLoaderDelegate>

@property (nonatomic, copy) NSString* gameObject;

- (instancetype) initWithGameObject:(NSString *)gameObject;

@end

//==============================================================================

@interface NADInterstitialEventDispatcher : NSObject <NADInterstitialDelegate>

+ (instancetype) sharedDispatcher;
- (void) dispatchShowResult:(NADInterstitialShowResult)result spotId:(NSString *)spotId;

@end

//==============================================================================

@interface BannerParams : NSObject

@property (nonatomic, copy) NSString* gameObject;
@property (nonatomic, copy) NSString* apiKey;
@property (nonatomic, copy) NSString* spotID;
@property (nonatomic) BOOL outputLog;
@property (nonatomic) NSInteger size;
@property (nonatomic) NSInteger gravity;
@property (nonatomic) NSInteger leftMargin;
@property (nonatomic) NSInteger topMargin;
@property (nonatomic) NSInteger rightMargin;
@property (nonatomic) NSInteger bottomMargin;

+ (instancetype) paramWithString:(NSString *)paramString;

@end

//==============================================================================

@interface Icon : NSObject

@property (nonatomic) NSInteger size;
@property (nonatomic) BOOL titleVisible;
@property (nonatomic) BOOL spaceEnabled;
@property (nonatomic, copy) NSString* titleColor;
@property (nonatomic) NSInteger gravity;
@property (nonatomic) NSInteger leftMargin;
@property (nonatomic) NSInteger topMargin;
@property (nonatomic) NSInteger rightMargin;
@property (nonatomic) NSInteger bottomMargin;

+ (instancetype) iconWithString:(NSString*)paramString;

@end

//==============================================================================

@interface IconParams : NSObject

@property (nonatomic, copy) NSString* gameObject;
@property (nonatomic, copy) NSString* apiKey;
@property (nonatomic, copy) NSString* spotID;
@property (nonatomic) BOOL outputLog;
@property (nonatomic) NSInteger orientation;
@property (nonatomic) NSInteger gravity;
@property (nonatomic) NSInteger leftMargin;
@property (nonatomic) NSInteger topMargin;
@property (nonatomic) NSInteger rightMargin;
@property (nonatomic) NSInteger bottomMargin;
@property (nonatomic) NSInteger iconCount;
@property (nonatomic, retain) NSMutableArray* iconArray;

+ (instancetype) paramWithString:(NSString *)paramString;

@end

//==============================================================================

@interface NendAdBanner : NSObject

@property (nonatomic, retain) NADView* bannerView;
@property (nonatomic, retain) BannerParams* params;

+ (instancetype) bannerAdWithParams:(BannerParams *)params;

- (void) load;
- (void) show;
- (void) hide;
- (void) resume;
- (void) pause;
- (void) layout;
- (void) willRotate:(NSNotification *)notification;
- (void) didRotate:(NSNotification *)notification;

@end

//==============================================================================

@interface NendAdIcon : NSObject

@property (nonatomic, retain) NADIconLoader* iconLoader;
@property (nonatomic, retain) NSMutableArray* iconViewArray;
@property (nonatomic, retain) IconParams* params;

+ (instancetype) iconAdWithParams:(IconParams *)params;

- (void) load;
- (void) show;
- (void) hide;
- (void) resume;
- (void) pause;
- (void) layout;
- (BOOL) isShowing;
- (void) willRotate:(NSNotification *)notification;
- (void) didRotate:(NSNotification *)notification;

@end

///-----------------------------------------------
/// @name Implementations
///-----------------------------------------------

@implementation NADViewEventDispatcher

- (instancetype) initWithGameObject:(NSString *)gameObject
{
    self = [super init];
    if ( self )
        _gameObject = [gameObject copy];
    return self;
}

- (void) dealloc
{
    [_gameObject release];
    [super dealloc];
}

- (void) nadViewDidFinishLoad:(NADView *)adView
{
    UnitySendMessage([self.gameObject UTF8String], "NendAdView_OnFinishLoad", "");
}

- (void) nadViewDidFailToReceiveAd:(NADView *)adView
{
    NSString* message = [NSString stringWithFormat:@"%d:%@", adView.error.code, adView.error.domain];
    UnitySendMessage([self.gameObject UTF8String], "NendAdView_OnFailToReceiveAd", MakeStringCopy([message UTF8String]));
}

- (void) nadViewDidReceiveAd:(NADView *)adView
{
    UnitySendMessage([self.gameObject UTF8String], "NendAdView_OnReceiveAd", "");
}

- (void) nadViewDidClickAd:(NADView *)adView
{
    UnitySendMessage([self.gameObject UTF8String], "NendAdView_OnClickAd", "");
}

@end

//==============================================================================

@implementation NADIconLoaderEventDispatcher

- (instancetype) initWithGameObject:(NSString *)gameObject
{
    self = [super init];
    if ( self )
        _gameObject = [gameObject copy];
    return self;
}

- (void) dealloc
{
    [_gameObject release];
    [super dealloc];
}

- (void) nadIconLoaderDidFinishLoad:(NADIconLoader *)iconLoader
{
    UnitySendMessage([self.gameObject UTF8String], "NendAdIconLoader_OnFinishLoad", "");
}

- (void) nadIconLoaderDidFailToReceiveAd:(NADIconLoader *)iconLoader nadIconView:(NADIconView *)nadIconView
{
    NSString* message = [NSString stringWithFormat:@"%d:%@", iconLoader.error.code, iconLoader.error.domain];
    UnitySendMessage([self.gameObject UTF8String], "NendAdIconLoader_OnFailToReceiveAd", MakeStringCopy([message UTF8String]));
}

- (void) nadIconLoaderDidReceiveAd:(NADIconLoader *)iconLoader nadIconView:(NADIconView *)nadIconView
{
    UnitySendMessage([self.gameObject UTF8String], "NendAdIconLoader_OnReceiveAd", "");
}

- (void) nadIconLoaderDidClickAd:(NADIconLoader *)iconLoader nadIconView:(NADIconView *)nadIconView
{
    UnitySendMessage([self.gameObject UTF8String], "NendAdIconLoader_OnClickAd", "");
}

@end

//==============================================================================

@implementation NADInterstitialEventDispatcher

+ (instancetype) sharedDispatcher
{
    static NADInterstitialEventDispatcher* instance;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        instance = [[NADInterstitialEventDispatcher alloc] init];
    });
    return instance;
}

- (void) dispatchShowResult:(NADInterstitialShowResult)result spotId:(NSString *)spotId
{
    NSInteger value = -1;
    switch ( result )
    {
        case AD_SHOW_SUCCESS:
            value = 0;
            break;
        case AD_LOAD_INCOMPLETE:
            value = 1;
            break;
        case AD_REQUEST_INCOMPLETE:
            value = 2;
            break;
        case AD_DOWNLOAD_INCOMPLETE:
            value = 3;
            break;
        case AD_FREQUENCY_NOT_REACHABLE:
            value = 4;
            break;
        case AD_SHOW_ALREADY:
            value = 5;
            break;
    }
    NSString* message = [NSString stringWithFormat:@"%d:%@", value, spotId];
    UnitySendMessage(INTERSTITIAL_GAME_OBJECT, "NendAdInterstitial_OnShowAd", MakeStringCopy([message UTF8String]));
}

- (void) didFinishLoadInterstitialAdWithStatus:(NADInterstitialStatusCode)status
{
    NSInteger value = InterstitialStatusCodeToInteger(status);
    NSString* message = [[NSNumber numberWithInteger:value] stringValue];
    UnitySendMessage(INTERSTITIAL_GAME_OBJECT, "NendAdInterstitial_OnFinishLoad", MakeStringCopy([message UTF8String]));
}

- (void) didFinishLoadInterstitialAdWithStatus:(NADInterstitialStatusCode)status spotId:(NSString *)spotId
{
    NSInteger value = InterstitialStatusCodeToInteger(status);
    NSString* message = [NSString stringWithFormat:@"%d:%@", value, spotId];
    UnitySendMessage(INTERSTITIAL_GAME_OBJECT, "NendAdInterstitial_OnFinishLoad", MakeStringCopy([message UTF8String]));
}

- (void) didClickWithType:(NADInterstitialClickType)type
{
    NSInteger value = InterstitialClickTypeToInteger(type);
    NSString* message = [[NSNumber numberWithInteger:value] stringValue];
    UnitySendMessage(INTERSTITIAL_GAME_OBJECT, "NendAdInterstitial_OnClickAd", MakeStringCopy([message UTF8String]));
}

- (void) didClickWithType:(NADInterstitialClickType)type spotId:(NSString *)spotId
{
    NSInteger value = InterstitialClickTypeToInteger(type);
    NSString* message = [NSString stringWithFormat:@"%d:%@", value, spotId];
    UnitySendMessage(INTERSTITIAL_GAME_OBJECT, "NendAdInterstitial_OnClickAd", MakeStringCopy([message UTF8String]));
}

@end

//==============================================================================

@implementation BannerParams

+ (instancetype) paramWithString:(NSString *)paramString
{
    return [[[BannerParams alloc] initWithParamString:paramString] autorelease];
}

- (instancetype) initWithParamString:(NSString *)paramString
{
    self = [super init];
    if ( self )
    {
        NSArray* paramArray = [paramString componentsSeparatedByString:@":"];
        int index = 0;
        _gameObject = [(NSString *)paramArray[index++] copy];
        _apiKey = [(NSString *)paramArray[index++] copy];
        _spotID = [(NSString *)paramArray[index++] copy];
        _outputLog = [@"true" isEqualToString:(NSString *)paramArray[index++]];
        _size = [paramArray[index++] integerValue];
        _gravity = [paramArray[index++] integerValue];
        _leftMargin = [paramArray[index++] integerValue];
        _topMargin = [paramArray[index++] integerValue];
        _rightMargin = [paramArray[index++] integerValue];
        _bottomMargin = [paramArray[index++] integerValue];
    }
    return self;
}

- (void) dealloc
{
    [_gameObject release];
    [_apiKey release];
    [_spotID release];
    [super dealloc];
}

@end

//==============================================================================

@implementation Icon

+ (instancetype) iconWithString:(NSString *)paramString
{
    return [[[Icon alloc] initWithParamString:paramString] autorelease];
}

- (instancetype) initWithParamString:(NSString *)paramString
{
    self = [super init];
    if ( self )
    {
        NSArray* paramArray = [paramString componentsSeparatedByString:@","];
        int index = 0;
        _size = [paramArray[index++] integerValue];
        _spaceEnabled = [@"true" isEqualToString:(NSString *)paramArray[index++]];
        _titleVisible = [@"true" isEqualToString:(NSString *)paramArray[index++]];
        _titleColor = [(NSString *)paramArray[index++] copy];;
        _gravity = [paramArray[index++] integerValue];
        _leftMargin = [paramArray[index++] integerValue];
        _topMargin = [paramArray[index++] integerValue];
        _rightMargin = [paramArray[index++] integerValue];
        _bottomMargin = [paramArray[index++] integerValue];
    }
    return self;
}

- (void) dealloc
{
    [_titleColor release];
    [super dealloc];
}

@end

//==============================================================================

@implementation IconParams

+ (instancetype) paramWithString:(NSString *)paramString
{
    return [[[IconParams alloc] initWithParamString:paramString] autorelease];
}

- (instancetype) initWithParamString:(NSString *)paramString
{
    self = [super init];
    if ( self )
    {
        NSArray* paramArray = [paramString componentsSeparatedByString:@":"];
        int index = 0;
        _gameObject = [(NSString *)paramArray[index++] copy];
        _apiKey = [(NSString *)paramArray[index++] copy];
        _spotID = [(NSString *)paramArray[index++] copy];
        _outputLog = [@"true" isEqualToString:(NSString *)paramArray[index++]];
        _orientation = [paramArray[index++] integerValue];
        _gravity = [paramArray[index++] integerValue];
        _leftMargin = [paramArray[index++] integerValue];
        _topMargin = [paramArray[index++] integerValue];
        _rightMargin = [paramArray[index++] integerValue];
        _bottomMargin = [paramArray[index++] integerValue];
        _iconCount = [paramArray[index++] integerValue];
        _iconArray = [[NSMutableArray alloc] init];
        for ( int i = 0; i < _iconCount; i++ )
            [_iconArray addObject:[Icon iconWithString:paramArray[index + i]]];
    }
    return self;
}

- (void) dealloc
{
    [_gameObject release];
    [_apiKey release];
    [_spotID release];
    [_iconArray release];
    [super dealloc];
}

@end

//==============================================================================

@implementation NendAdBanner

+ (instancetype) bannerAdWithParams:(BannerParams *)params
{
    return [[[NendAdBanner alloc] initWithParams:params] autorelease];
}

- (instancetype) initWithParams:(BannerParams *)params;
{
    self = [super init];
    if ( self )
    {
        _params = [params retain];
        
        _bannerView = [[NADView alloc] init];
        _bannerView.hidden = YES;
        _bannerView.delegate = [[NADViewEventDispatcher alloc] initWithGameObject:params.gameObject];
        _bannerView.isOutputLog = params.outputLog;
        
        [_bannerView setNendID:params.apiKey spotID:params.spotID];
        
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(willRotate:) name:kUnityViewWillRotate object:nil];
    }
    
    return self;
}

- (void) dealloc
{
    [[NSNotificationCenter defaultCenter] removeObserver:self];
    
    [_bannerView removeFromSuperview];
    
    [_bannerView.delegate release];
    _bannerView.delegate = nil;
    
    [_bannerView release];
    [_params release];
    
    _bannerView = nil;
    _params = nil;
    
    [super dealloc];
}

- (void) load
{
    if ( self.bannerView )
        [self.bannerView load];
}

- (void) show
{
    if ( self.bannerView && self.bannerView.hidden )
    {
        [self layout];
        self.bannerView.hidden = NO;
    }
}

- (void) hide
{
    if ( self.bannerView && !self.bannerView.hidden )
        self.bannerView.hidden = YES;
}

- (void) resume
{
    if ( self.bannerView )
        [self.bannerView resume];
}

- (void) pause
{
    if ( self.bannerView )
        [self.bannerView pause];
}

- (void) layout
{
    if ( !self.bannerView )
        return;
    
    CGSize bannerSize = BannerSize((NendBannerSize)self.params.size);
    CGPoint point = CalculatePointFromGravityAndMargins(self.params.gravity,
                                                        bannerSize,
                                                        self.params.leftMargin,
                                                        self.params.topMargin,
                                                        self.params.rightMargin,
                                                        self.params.bottomMargin);
    
    self.bannerView.frame = CGRectMake(point.x, point.y, bannerSize.width, bannerSize.height);
}

- (void) willRotate:(NSNotification *)notification
{
    if ( !self.bannerView || self.bannerView.hidden )
        return;
    
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(didRotate:) name:kUnityViewDidRotate object:nil];
    [self hide];
}

- (void) didRotate:(NSNotification *)notification
{
    [[NSNotificationCenter defaultCenter] removeObserver:self name:kUnityViewDidRotate object:nil];
    [self show];
}

@end

//==============================================================================

@implementation NendAdIcon

+ (instancetype) iconAdWithParams:(IconParams *)params
{
    return [[[NendAdIcon alloc] initWithParams:params] autorelease];
}

- (instancetype) initWithParams:(IconParams *)params
{
    self = [super init];
    if ( self )
    {
        _params = [params retain];
        
        _iconViewArray = [[NSMutableArray alloc] init];
        
        _iconLoader = [[NADIconLoader alloc] init];
        _iconLoader.isOutputLog = params.outputLog;
        _iconLoader.delegate = [[NADIconLoaderEventDispatcher alloc] initWithGameObject:params.gameObject];
        
        for ( int i = 0; i < params.iconCount ; i++ )
        {
            Icon* icon = params.iconArray[i];
            
            NADIconView* iconView = [[[NADIconView alloc] init] autorelease];
            iconView.hidden = YES;
            iconView.textColor = CreateUIColorFromColorCode(icon.titleColor);
            iconView.textHidden = !icon.titleVisible;
            iconView.iconSpaceEnabled = icon.spaceEnabled;
            
            [_iconLoader addIconView:iconView];
            [_iconViewArray addObject:iconView];
        }
        
        [_iconLoader setNendID:params.apiKey spotID:params.spotID];
        
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(willRotate:) name:kUnityViewWillRotate object:nil];
    }
    
    return self;
}

- (void) dealloc
{
    [[NSNotificationCenter defaultCenter] removeObserver:self];
    
    for ( NADIconView* iconView in _iconViewArray )
    {
        [iconView removeFromSuperview];
        [_iconLoader removeIconView:iconView];
    }
    [_iconViewArray removeAllObjects];
    
    [_iconLoader.delegate release];
    _iconLoader.delegate = nil;
    
    [_iconLoader release];
    [_iconViewArray release];
    [_params release];
    
    _iconLoader = nil;
    _iconViewArray = nil;
    _params = nil;
    
    [super dealloc];
}

- (void) load
{
    if ( self.iconLoader )
        [self.iconLoader load];
}

- (void) show
{
    if ( self.iconViewArray && ![self isShowing] )
    {
        [self layout];
        for ( NADIconView* iconView in self.iconViewArray )
            iconView.hidden = NO;
    }
}

- (void) hide
{
    if ( self.iconViewArray && [self isShowing] )
    {
        for ( NADIconView* iconView in self.iconViewArray )
            iconView.hidden = YES;
    }
}

- (void) resume
{
    if ( self.iconLoader )
        [self.iconLoader resume];
}

- (void) pause
{
    if ( self.iconLoader )
        [self.iconLoader pause];
}

- (void) layout
{
    if ( UNSPECIFIED != self.params.orientation )
    {
        CGFloat width = 0.0f;
        CGFloat height = 0.0f;
        
        if ( VERTICAL == self.params.orientation )
        {
            CGFloat iconWidth = 0.0f;
            for ( Icon* icon in self.params.iconArray )
            {
                height += (IconActualHeight(icon.size, icon.titleVisible, icon.spaceEnabled) + icon.topMargin + icon.bottomMargin);
                iconWidth = icon.size + icon.leftMargin + icon.rightMargin;
                if ( width < iconWidth )
                    width = iconWidth;
            }
        }
        else
        {
            CGFloat iconHeight = 0.0f;
            for ( Icon* icon in self.params.iconArray )
            {
                width += (icon.size + icon.leftMargin + icon.rightMargin);
                iconHeight = IconActualHeight(icon.size, icon.titleVisible, icon.spaceEnabled) + icon.topMargin + icon.bottomMargin;
                if ( height < iconHeight )
                    height = iconHeight;
            }
        }
        
        CGPoint point = CalculatePointFromGravityAndMargins(self.params.gravity,
                                                            CGSizeMake(width, height),
                                                            self.params.leftMargin,
                                                            self.params.topMargin,
                                                            self.params.rightMargin,
                                                            self.params.bottomMargin);
        
        for ( int i = 0; i < self.iconViewArray.count && i < self.params.iconArray.count; i++ )
        {
            Icon* icon = self.params.iconArray[i];
            NADIconView* iconView = self.iconViewArray[i];
            
            CGRect frame;
            if ( VERTICAL == self.params.orientation )
            {
                frame = CGRectMake(point.x + icon.leftMargin - icon.rightMargin, point.y + icon.topMargin, icon.size, icon.size);
                point.y += icon.topMargin;
                point.y += icon.size;
                point.y += icon.bottomMargin;
            }
            else
            {
                frame = CGRectMake(point.x + icon.leftMargin, point.y + icon.topMargin - icon.bottomMargin, icon.size, icon.size);
                point.x += icon.leftMargin;
                point.x += icon.size;
                point.x += icon.rightMargin;
            }
            
            iconView.frame = frame;
        }
    }
    else
    {
        for ( int i = 0; i < self.iconViewArray.count && i < self.params.iconArray.count; i++ )
        {
            Icon* icon = self.params.iconArray[i];
            NADIconView* iconView = self.iconViewArray[i];
            
            CGPoint point = CalculatePointFromGravityAndMargins(icon.gravity,
                                                                CGSizeMake(icon.size, IconActualHeight(icon.size, icon.titleVisible, icon.spaceEnabled)),
                                                                icon.leftMargin,
                                                                icon.topMargin,
                                                                icon.rightMargin,
                                                                icon.bottomMargin);
            
            iconView.frame = CGRectMake(point.x, point.y, icon.size, icon.size);
        }
    }
}

- (void) willRotate:(NSNotification *)notification
{
    if ( !self.iconViewArray || ![self isShowing] )
        return;
    
    [self hide];
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(didRotate:) name:kUnityViewDidRotate object:nil];
}

- (void) didRotate:(NSNotification *)notification
{
    [[NSNotificationCenter defaultCenter] removeObserver:self name:kUnityViewDidRotate object:nil];
    [self show];
}

- (BOOL) isShowing
{
    if ( !self.iconViewArray )
        return NO;
    
    for ( NADIconView* iconView in self.iconViewArray )
    {
        if ( iconView.hidden )
            return NO;
    }
    return YES;
}

@end

///-----------------------------------------------
/// @name C Interfaces
///-----------------------------------------------

extern "C"
{
    ///-----------------------------------------------
    /// @name Banner
    ///-----------------------------------------------

	void _TryCreateBanner(const char* paramString)
    {
        BOOL didLoaded = NO;
        BannerParams* params = [BannerParams paramWithString:CreateNSString(paramString)];
        NendAdBanner* ad = _bannerAdDict[params.gameObject];
        
        if ( !ad )
        {
            ad = [NendAdBanner bannerAdWithParams:params];
            _bannerAdDict[params.gameObject] = ad;
        }
        else
            didLoaded = YES;
        
        if ( !ad.bannerView.superview )
            [UnityGetGLView() addSubview:ad.bannerView];
        
        if ( !didLoaded )
            [ad.bannerView load];
    }
    
    void _ShowBanner(const char* gameObject)
    {
        NendAdBanner* ad = _bannerAdDict[CreateNSString(gameObject)];
        if ( ad )
            [ad show];
    }
    
	void _HideBanner(const char* gameObject)
    {
        NendAdBanner* ad = _bannerAdDict[CreateNSString(gameObject)];
        if ( ad )
            [ad hide];
    }
    
	void _ResumeBanner(const char* gameObject)
    {
        NendAdBanner* ad = _bannerAdDict[CreateNSString(gameObject)];
        if ( ad )
            [ad resume];
    }
    
	void _PauseBanner(const char* gameObject)
    {
        NendAdBanner* ad = _bannerAdDict[CreateNSString(gameObject)];
        if ( ad )
            [ad pause];
    }
    
	void _DestroyBanner(const char* gameObject)
    {
        [_bannerAdDict removeObjectForKey:CreateNSString(gameObject)];
    }
    
    ///-----------------------------------------------
    /// @name Icon
    ///-----------------------------------------------

    void _TryCreateIcons(const char* paramString)
    {
        BOOL didLoaded = NO;
        IconParams* params = [IconParams paramWithString:CreateNSString(paramString)];
        NendAdIcon* ad = _iconAdDict[params.gameObject];
        
        if ( !ad )
        {
            ad = [NendAdIcon iconAdWithParams:params];
            _iconAdDict[params.gameObject] = ad;
        }
        else
            didLoaded = YES;
        
        for ( NADIconView* iconView in ad.iconViewArray )
        {
            if ( !iconView.superview )
                [UnityGetGLView() addSubview:iconView];
        }
        
        if ( !didLoaded )
            [ad load];
    }
    
    void _ShowIcons(const char* gameObject)
    {
        NendAdIcon* ad = _iconAdDict[CreateNSString(gameObject)];
        if ( ad )
            [ad show];
    }
    
	void _HideIcons(const char* gameObject)
    {
        NendAdIcon* ad = _iconAdDict[CreateNSString(gameObject)];
        if ( ad )
            [ad hide];
    }
    
	void _ResumeIcons(const char* gameObject)
    {
        NendAdIcon* ad = _iconAdDict[CreateNSString(gameObject)];
        if ( ad )
            [ad resume];
    }
    
	void _PauseIcons(const char* gameObject)
    {
        NendAdIcon* ad = _iconAdDict[CreateNSString(gameObject)];
        if ( ad )
            [ad pause];
    }
    
	void _DestroyIcons(const char* gameObject)
    {
        [_iconAdDict removeObjectForKey:CreateNSString(gameObject)];
    }
    
    ///-----------------------------------------------
    /// @name Interstitial
    ///-----------------------------------------------

    void _LoadInterstitialAd(const char* apiKey, const char* spotId, BOOL isOutputLog)
    {
        AddRotateMethodToInterstitialViewController();
        
        [NADInterstitial sharedInstance].isOutputLog = isOutputLog;
        [NADInterstitial sharedInstance].delegate = [NADInterstitialEventDispatcher sharedDispatcher];
        
        [[NADInterstitial sharedInstance] loadAdWithApiKey:CreateNSString(apiKey) spotId:CreateNSString(spotId)];
    }
    
    void _ShowInterstitialAd(const char* spotId)
    {
        NSString* spot = CreateNSString(spotId);
        NADInterstitialShowResult result;
        if ( spot && 0 < spot.length )
            result = [[NADInterstitial sharedInstance] showAdWithSpotId:spot];
        else
            result = [[NADInterstitial sharedInstance] showAd];
        
        [[NADInterstitialEventDispatcher sharedDispatcher] dispatchShowResult:result spotId:spot];
    }
    
    void _DismissInterstitialAd()
    {
        [[NADInterstitial sharedInstance] dismissAd];
    }
}
