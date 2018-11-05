import React from 'react'
import {
    View,
    Modal,
    TouchableOpacity,
    Image,
    Text,
    UIManager,
    Platform,
    ViewPropTypes,
    ScrollView,
} from 'react-native'
import PropTypes from 'prop-types'
import Icon from 'react-native-vector-icons/FontAwesome'

import { Colors, Fonts, Images, Metrics } from '../../Themes'
import styles from './Styles'
import GradientView from '../GradientView'
import GradientHeader, { Header } from '../GradientHeader'
import AnimatedTouchable from '../AnimatedTouchable'
import SearchBox from '../SearchBox'


const viewPropTypes = ViewPropTypes || View.propTypes;

export default class Picker extends React.Component {
    static propTypes = {
        onValueChanged: PropTypes.func.isRequired,
        style: viewPropTypes.style,
        isVisible: PropTypes.bool
    }

    static defaultProps = {
        onValueChanged: (value) => { },
    }

    constructor(props) {
        super(props)
        this.state = {
            isVisible: props.isVisible || false,
        }

        if (Platform.OS === 'android') {
            UIManager.setLayoutAnimationEnabledExperimental(true);
        }
        this._bootStrapAsync()
    }

    async _bootStrapAsync() {
        // this.props.ref && this.props.ref(this)
    }

    render() {
        const {
            value,
            style,
            searchEnabled,
            touchableComponent
        } = this.props
        const { width } = this.state

        let scrollEnabled = this.props.scrollEnabled
        if (typeof scrollEnabled == undefined)
            scrollEnabled = true

        return (
            <View
                style={[style]}
                onLayout={this.onLayout.bind(this)}
            >
                <AnimatedTouchable onPress={this.show}>
                    {
                        touchableComponent
                            ?
                            touchableComponent
                            :
                            (
                                <View style={[styles.button]}>
                                    <Text style={styles.label}>
                                        {value}
                                    </Text>
                                    <View style={styles.icon}><Icon size={25} color='#000' name={'angle-right'} /></View>
                                </View>
                            )
                    }
                </AnimatedTouchable>

                <Modal
                    animationType="slide"
                    visible={this.state.isVisible}
                    onRequestClose={this.dismiss}>
                    <GradientView style={[styles.linearGradient]}>
                        <GradientHeader>
                            <Header title={this.props.title}
                                goBack={this.dismiss} />
                        </GradientHeader>
                        {searchEnabled && <SearchBox />}
                        <ScrollView
                            scrollEventThrottle={10}
                            scrollEnabled={scrollEnabled}
                            style={{ flex: 1 }}
                        >
                            {this.props.children}
                        </ScrollView>
                    </GradientView>
                </Modal>
            </View>
        )
    }

    show = () => {
        this.setState({ isVisible: true })
    }

    dismiss = () => {
        this.setState({ isVisible: false })
    }

    onLayout = (e) => {
        const viewWidth = e.nativeEvent.layout.width
    }
}
