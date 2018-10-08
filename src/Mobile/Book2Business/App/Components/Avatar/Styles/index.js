import { StyleSheet } from 'react-native'
import { ApplicationStyles, Metrics, Colors, Fonts } from '../../../Themes/'

export default StyleSheet.create({
    ...ApplicationStyles.screen,
    container: {
        alignItems: 'center',
        flexDirection: 'row',
    },
    image: {
        width: 40,
        height: 40,
    },
    badge: {
        width: 15,
        height: 15,
        borderRadius: 7.5,
        alignItems: 'center',
        justifyContent: 'center',
        position: 'absolute',
        bottom: -2,
        right: -2,
    },
    badgeText: {
        backgroundColor: 'transparent',
        fontSize: 9,
    },
    bigImage: {
        width: 110,
        height: 110,
        borderRadius: 55,
        marginBottom: 19,
    },
    smallImage: {
        width: 32,
        height: 32,
        borderRadius: 16,
    },
    circleImage: {
        borderRadius: 20,
    },
})
