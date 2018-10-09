import { StyleSheet } from 'react-native'
import { ApplicationStyles, Metrics, Colors } from '../../../Themes'

export default StyleSheet.create({
    ...ApplicationStyles.screen,
    root: {
        flex: 1,
        backgroundColor: Colors.snow,
    },
    container:{

        alignItems: 'center',
    },
    header: {
        backgroundColor: Colors.snow,
        paddingVertical: 25,
    },
    section: {
        marginVertical: 25,
    },
    heading: {
        paddingBottom: 12.5,
    },
    row: {
        flexDirection: 'row',
        paddingHorizontal: 17.5,
        borderBottomWidth: StyleSheet.hairlineWidth,
        borderColor: Colors.snow,
        alignItems: 'center',
    },
    button: {
        marginHorizontal: 16,
        marginBottom: 32,
        backgroundColor: Colors.purple,
        width: '90%'
    },
    primary: {
        color: Colors.redShadow,
        fontWeight: 'bold'
    }
});