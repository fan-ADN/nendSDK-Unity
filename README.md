nendSDK-Unity Sample Project
================== 
![ロゴ](https://github.com/fan-ADN/nendSDK-Android/blob/master/Sample/res/drawable/nend_logo.png)

Overview
---------------------------------
nendSDK_Unityプラグインのサンプルプロジェクトです。  
基本的な実装方法および動作の確認が行えます。

Description
---------------------------------
Androidにおけるビルド方法の違いに応じて2種類のサンプルをご用意しています。
* NendUnitySampleA  
Androidプロジェクトにエクスポートしてから動作させる場合

* NendUnitySampleU  
Unityから直接apkファイルを作成し動作させる場合

※サンプルの動作自体に差異はありません  
※iOSについては両サンプルの内容に差異はありません  
※動作確認は実機上で行ってください。UnityPlayerおよびシミュレータでは広告が表示されません

Requirement
---------------------------------
* NendUnitySampleA(Android)  
Androidプロジェクトにエクスポート後、Android公式リファレンスやnendSDKのマニュアル等を参考に、  
Google Play servicesライブラリをプロジェクトに追加してください。  

Usage
---------------------------------
**Android**

* NendUnitySampleA  
Androidプロジェクトにエクスポート後、Google Play servicesライブラリを追加し、  
AndroidManifest.xml内の以下コメントアウトを外してビルドしてください。

`<!-- <meta-data android:name="com.google.android.gms.version" android:value="@integer/google_play_services_version" /> -->`

* NendUnitySampleU  
Unityからapkファイルを作成し、実機にインストールしてください。  
  
**iOS**  
* Xcodeプロジェクトにエクスポートしてビルドしてください。
