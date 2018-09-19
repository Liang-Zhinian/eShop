#import <UIKit/UIKit.h>
#import "MSPushDelegate.h"
#import "MSServiceAbstract.h"

NS_ASSUME_NONNULL_BEGIN

/**
 * Mobile Center push service.
 */
@interface MSPush : MSServiceAbstract

/**
 * Callback for successful registration with push token.
 *
 * @param deviceToken The device token for remote notifications.
 */
+ (void)didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken;

/**
 * Callback for unsuccessful registration with error.
 *
 * @param error Error of unsuccessful registration.
 */
+ (void)didFailToRegisterForRemoteNotificationsWithError:(NSError *)error;

/**
 * Callback for notification with user info.
 *
 * @param userInfo The user info for the remote notification.
 *
 * @return YES if the notification was sent via Mobile Center.
 */
+ (BOOL)didReceiveRemoteNotification:(NSDictionary *)userInfo;

/**
 * Set the delegate.
 *
 * Defines the class that implements the optional protocol `MSPushDelegate`.
 *
 * @param delegate Sender's delegate.
 *
 * @see MSPushDelegate
 */
+ (void)setDelegate:(nullable id<MSPushDelegate>)delegate;

NS_ASSUME_NONNULL_END

@end
