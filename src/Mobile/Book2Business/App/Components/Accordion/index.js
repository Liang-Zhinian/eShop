import React from 'react'
import {
    View,
    TouchableOpacity,
    ScrollView,
    Image,
    Text,
    Animated,
    LayoutAnimation,
    UIManager,
    Platform
} from 'react-native'
import PropTypes from 'prop-types'
import { format } from 'date-fns'
import Icon from 'react-native-vector-icons/FontAwesome'
import { Colors, Fonts, Images, } from '../../Themes/'
import styles from './Styles'

const dataArray = [
    { title: "First Element", content: "Lorem ipsum dolor sit amet" },
    { title: "Second Element", content: "Lorem ipsum dolor sit amet" },
    { title: "Third Element", content: "Lorem ipsum dolor sit amet" }
];

export default class Accordion extends React.Component {
    static propTypes = {
        onValueChanged: PropTypes.func.isRequired,
    }

    static defaultProps = {
        onValueChanged: (value) => { },
    }

    constructor(props) {
        super(props)
        this.state = {
            selectedIndex: props.activeIndex || 0,

        }

        this._bootStrapAsync = this._bootStrapAsync.bind(this)
        this.onPress = this.onPress.bind(this)
        this.renderItem = this.renderItem.bind(this)

        if (Platform.OS === 'android') {
            UIManager.setLayoutAnimationEnabledExperimental(true);
        }
        this._bootStrapAsync()
    }

    async _bootStrapAsync() {

    }

    render() {

        const { event } = Animated
        return (
            <ScrollView
                ref='scrolly'
                onScroll={event([{ nativeEvent: { contentOffset: { y: this.state.scrollY } } }])}
                scrollEventThrottle={10}
                scrollEnabled={true}>
                <View style={[styles.mainContainer,]}>
                    {
                        this.props.data && this.props.data.map((item, index) => {
                            return this.renderItem({ item, index })
                        })
                    }
                </View>
            </ScrollView>
        )
    }

    renderTitle({ item, index }) {
        return (
            <View style={styles.title}>
                <Text style={[styles.label, styles.TouchableOpacityTitleText]}>
                    {this.props.titleExtractor && this.props.titleExtractor(item)}
                </Text>
                {/* <Image
                    style={[styles.icon, this.state.selectedIndex == index && styles.flip]}
                    source={Images.chevronIcon}
                /> */}
                <Icon size={25} color='#fff' name={this.state.selectedIndex == index ? 'angle-down' : 'angle-right'} />
            </View>
        )
    }

    renderItem({ item, index }) {
        return (
            <View key={item.Id} style={{ flex: 1 }}>
                <TouchableOpacity activeOpacity={0.7}
                    onPress={() => this.onPress({ item, index })}
                    style={styles.TouchableOpacityStyle}>
                    {this.props.renderTitle ? this.props.renderTitle({ item, index }) : this.renderTitle({ item, index })}
                </TouchableOpacity>
                <View style={[styles.content, { display: (this.state.selectedIndex == index ? 'flex' : 'none') }]}>
                    {this.props.renderItem && this.props.renderItem({ item, index })}
                </View>
            </View>
        );
    }

    onPress = ({ item, index }) => {
        LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);

        this.setState({
            selectedIndex: index
        })
        
        this.props.onPress && this.props.onPress(item)
    }

    onValueReturned = () => {
        // setTimeout(() => { this.props.onValueChanged && this.props.onValueChanged(this.state.markedDates) }, 500)
    }
}
