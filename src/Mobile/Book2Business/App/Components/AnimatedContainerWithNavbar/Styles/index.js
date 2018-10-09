import { StyleSheet, Dimensions } from 'react-native'
const { width, height } = Dimensions.get('window')

export default StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#555566'
        // paddingTop: 20
    },
    content: {
        zIndex: 3
    },
    containerRow: {
        marginBottom: 10
    },
    listView: {
        marginHorizontal: 10
    },
    imageFooter: {
        backgroundColor: '#555566',
        flexDirection: 'row',
        paddingHorizontal: 7,
        paddingVertical: 7
    },
    textFooter: {
        color: '#fff'
    },
    textFooterBold: {
        fontWeight: 'bold'
    },
    imageAvatar: {
        width: 50,
        height: 50,
        borderRadius: 25,
        marginRight: 7
    },
    textFooterLight: {
        fontWeight: '100'
    },
    textFooterLittle: {
        fontSize: 11
    },
    menutext: {
        fontSize: 20,
        padding: 10
    },
    menu: {
        width: width,
        height: height,
        flex: 1,
        position: 'absolute',
        left: 0,
        top: 0,
        backgroundColor: '#ff00ff'
    },
    menulist: {
        width: 200,
        position: 'absolute',
        right: 0,
        top: 100
    }
})
