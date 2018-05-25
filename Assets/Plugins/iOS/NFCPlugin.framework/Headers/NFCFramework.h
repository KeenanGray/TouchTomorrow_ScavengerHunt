#import <Foundation/Foundation.h>

@protocol NFCDelegate <NSObject>
- (void)newNFCMessageAvailable:(const char*)message;
- (void)newDialogueResultAvailable:(bool)response;
@end
@interface NFCFramework : NSObject
+ (void)initPlugin;
+ (void)beginNFCSession;
+ (void)displayFrameworkString:(NSString *)string;
+(void)setDelegate:(id<NFCDelegate>)delegate;
+(void) tagScanned2;

@end
