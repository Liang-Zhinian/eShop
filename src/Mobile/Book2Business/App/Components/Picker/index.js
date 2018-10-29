import React from 'react'
import {
    View,
    Modal,
    TouchableOpacity,
    Image,
    Text,
    UIManager,
    Platform
} from 'react-native'
import { Container, Body } from 'native-base'
import PropTypes from 'prop-types'

import { Colors, Fonts, Images, } from '../../Themes'
import styles from './Styles'
import GradientHeader, { Header } from '../GradientHeader'

export default class Picker extends React.Component {
    static propTypes = {
        onValueChanged: PropTypes.func.isRequired,
    }

    static defaultProps = {
        onValueChanged: (value) => { },
    }

    constructor(props) {
        super(props)
        this.state = {
            showModal: props.showModal || false,
        }

        if (Platform.OS === 'android') {
            UIManager.setLayoutAnimationEnabledExperimental(true);
        }
        this._bootStrapAsync()
    }

    async _bootStrapAsync() {
        this.props.ref && this.props.ref(this)
    }

    render() {
        const {
            value,
        } = this.props

        return (
            <View style={styles.mainContainer}>
                <TouchableOpacity onPress={this.show} style={styles.button}>
                    <Text style={styles.label}>
                        {value}
                    </Text>
                    <Image
                        style={[styles.icon]}
                        source={Images.chevronRight}
                        esizeMode='cover'
                    />
                </TouchableOpacity>

                <Modal
                    animationType="slide"
                    visible={this.state.showModal}
                    onRequestClose={this.dismiss}>
                    <Container>
                        <GradientHeader>
                            <Header title={this.props.title}
                                goBack={this.dismiss} />
                        </GradientHeader>
                        <Body>
                            <View style={{ flex: 1, flexDirection: 'row' }}>
                                {this.props.children}
                            </View>
                        </Body>
                    </Container>
                </Modal>
            </View>
        )
    }

    show = () => {
        this.setState({ showModal: true })
    }

    dismiss = () => {
        this.setState({ showModal: false })
    }
}
