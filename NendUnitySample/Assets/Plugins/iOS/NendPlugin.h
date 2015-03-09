//
//  NendPlugin.h
//  Unity-iPhone
//

#import <Foundation/Foundation.h>

#import "NADView.h"
#import "NADIconView.h"
#import "NADIconLoader.h"
#import "NADInterstitial.h"


///-----------------------------------------------
/// @name Interfaces
///-----------------------------------------------

@interface NADViewEventDispatcher : NSObject <NADViewDelegate>

@property (nonatomic, copy) NSString* gameObject;

- (instancetype)initWithGameObject:(NSString*)gameObject;

@end

//==============================================================================

@interface NADIconLoaderEventDispatcher : NSObject <NADIconLoaderDelegate>

@property (nonatomic, copy) NSString* gameObject;

- (instancetype)initWithGameObject:(NSString*)gameObject;

@end

//==============================================================================

@interface NADInterstitialEventDispatcher : NSObject <NADInterstitialDelegate>

+ (instancetype)sharedDispatcher;
- (void)dispatchShowResult:(NADInterstitialShowResult)result spotId:(NSString*)spotId;

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

+ (instancetype)paramWithString:(NSString*)paramString;
- (void)updateLayoutWithString:(NSString*)paramString;

@end

//==============================================================================

@interface IconParams : NSObject

@property (nonatomic) NSInteger tag;
@property (nonatomic) NSInteger size;
@property (nonatomic) BOOL titleVisible;
@property (nonatomic) BOOL spaceEnabled;
@property (nonatomic, copy) NSString* titleColor;
@property (nonatomic) NSInteger gravity;
@property (nonatomic) NSInteger leftMargin;
@property (nonatomic) NSInteger topMargin;
@property (nonatomic) NSInteger rightMargin;
@property (nonatomic) NSInteger bottomMargin;

+ (instancetype)paramWithString:(NSString*)paramString;
- (BOOL)updateLayoutWithString:(NSString*)paramString;

@end

//==============================================================================

@interface IconGroupParams : NSObject

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

+ (instancetype)paramWithString:(NSString*)paramString;
- (void)updateLayoutWithString:(NSString*)paramString;

@end

//==============================================================================

@interface NendRotationHandler : NSObject

@property (nonatomic, copy) dispatch_block_t block;

- (void)startHandlingUsingBlock:(dispatch_block_t)block;
- (void)stopHandling;

@end

//==============================================================================

@interface NendAdBanner : NSObject

@property (nonatomic, retain) NADView* bannerView;
@property (nonatomic, retain) BannerParams* params;
@property (nonatomic, retain) NendRotationHandler* rotationHandler;

+ (instancetype)bannerAdWithParams:(BannerParams*)params;

- (void)load;
- (void)show;
- (void)hide;
- (void)resume;
- (void)pause;
- (void)updateLayoutWithString:(NSString*)paramString;

@end

//==============================================================================

@interface NendAdIcon : NSObject

@property (nonatomic, retain) NADIconLoader* iconLoader;
@property (nonatomic, retain) NSMutableArray* iconViewArray;
@property (nonatomic, retain) IconGroupParams* params;
@property (nonatomic, retain) NendRotationHandler* rotationHandler;

+ (instancetype)iconAdWithParams:(IconGroupParams*)params;

- (void)load;
- (void)show;
- (void)hide;
- (void)resume;
- (void)pause;
- (void)updateLayoutWithString:(NSString*)paramString;

@end

///-----------------------------------------------
/// @name C Interfaces
///-----------------------------------------------

extern "C" {
    
    ///-----------------------------------------------
    /// @name Banner
    ///-----------------------------------------------
    void _TryCreateBanner(const char* paramString);
    void _ShowBanner(const char* gameObject);
    void _HideBanner(const char* gameObject);
    void _ResumeBanner(const char* gameObject);
    void _PauseBanner(const char* gameObject);
    void _DestroyBanner(const char* gameObject);
    void _LayoutBanner(const char* gameObject, const char* paramString);
    
    ///-----------------------------------------------
    /// @name Icon
    ///-----------------------------------------------
    void _TryCreateIcons(const char* paramString);
    void _ShowIcons(const char* gameObject);
    void _HideIcons(const char* gameObject);
    void _ResumeIcons(const char* gameObject);
    void _PauseIcons(const char* gameObject);
    void _DestroyIcons(const char* gameObject);
    void _LayoutIcons(const char* gameObject, const char* paramString);
    
    ///-----------------------------------------------
    /// @name Interstitial
    ///-----------------------------------------------
    void _LoadInterstitialAd(const char* apiKey, const char* spotId, BOOL isOutputLog);
    void _ShowInterstitialAd(const char* spotId);
    void _DismissInterstitialAd();
}