import { StyleSheet } from 'react-native'
import { ApplicationStyles, Metrics, Colors } from '../../../Themes'

export default StyleSheet.create({
    ...ApplicationStyles.screen,
    container: {
        flex: 1,
        backgroundColor: Colors.snow
    },
    row: {
        flex: 1,
        backgroundColor: Colors.snow,
        marginVertical: Metrics.smallMargin
    },
    boldLabel: {
        fontWeight: 'bold',
        color: Colors.text
    },
    label: {
        color: Colors.text
    },
    listContent: {
        paddingTop: Metrics.baseMargin,
        paddingBottom: Metrics.baseMargin * 8
    },
    timeline: {
        width: 2,
        backgroundColor: '#6E3C7B',
        position: 'absolute',
        top: 85,
        bottom: 0,
        right: 11
    },
    item: {
        backgroundColor: '#c6c6c6',
        flex: 1,
        borderRadius: 5,
        margin: 2,
        padding: 2
    },
    emptyDate: {
        height: 15,
        flex: 1,
        // paddingTop: 30
    },
    when: {
        fontSize: 12
    },
    who: {
        fontWeight: 'bold',
        fontSize: 12
    },
    what: {
        fontSize: 12
    },
    where: {
        fontSize: 12
    },
    addButton: {
        flex: 1,
        width: Metrics.screenWidth * 0.15,
        height: Metrics.screenWidth * 0.15,
        position: 'absolute',
        right: 10,
        bottom: 10,
        borderRadius: Metrics.screenWidth * 0.075,
        borderColor: 'white',
        borderWidth: 2,
        backgroundColor: '#00adf5',
        justifyContent: 'center',
        alignItems: 'center',

    }
})
