import React from 'react'
import { TouchableOpacity } from 'react-native'
import Icon from 'react-native-vector-icons/FontAwesome'

export default class Hamburger extends React.Component {

    constructor(props) {
        super(props)

        this.state = {
            isMenuOpen: false,
        }
    }

    render() {
        const { size, color, style } = this.props
        return (
            <TouchableOpacity style={[{ marginRight: 20 }, style]} onPress={this.onPress.bind(this)} >
                <Icon name={this.state.isMenuOpen ? 'times' : 'bars'} size={size || 20} color={color || '#fff'} />
            </TouchableOpacity >
        )
    }

    onPress() {
        let isMenuOpen = this.state.isMenuOpen
        isMenuOpen = !isMenuOpen
        this.setState({ isMenuOpen: isMenuOpen })
        this.props.onPress && this.props.onPress(isMenuOpen)
    }
}