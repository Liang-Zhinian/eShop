import React from 'react';
import {
    Image,
    View,
    Text,
    TouchableOpacity
} from 'react-native';
import FontAwesome from 'react-native-vector-icons/FontAwesome';
import styles from './Styles';

export default class Avatar extends React.Component {
    componentName = 'Avatar';
    typeMapping = {
        container: {},
        image: {},
        badge: {},
        badgeText: {},
    };

    getBadgeStyle = (badgeProps) => {
        switch (badgeProps) {
            case 'like':
                return {
                    symbol: 'heart',
                    backgroundColor: 'white',
                    color: 'gray',
                };
            case 'follow':
                return {
                    symbol: 'plus',
                    backgroundColor: 'white',
                    color: 'gray',
                };
            default: return {};
        }
    };

    renderImg = (styles) => {
        let sizedImage = styles.bigImage
        switch (styles.size) {
            case 'big':
                sizedImage = styles.bigImage
                break;
            case 'small':
                sizedImage = styles.smallImage
                break;
            case 'circle':
                sizedImage = styles.circleImage
                break;
        }
        return (
            <View>
                <Image style={[styles.image, sizedImage]} source={this.props.img} />
                {this.props.badge && this.renderBadge(styles.badge)}
            </View>
        );
    }

    renderBadge = (style, textStyle) => {
        const badgeStyle = this.getBadgeStyle(this.props.badge);
        return (
            <View style={[style, { backgroundColor: badgeStyle.backgroundColor }]}>
                <TouchableOpacity style={[textStyle, { color: badgeStyle.color }]}>
                    <FontAwesome name={badgeStyle.symbol} />
                </TouchableOpacity>
            </View>
        );
    };

    render() {
        const { container, ...other } = styles;
        return (
            <View style={[container, this.props.style]}>
                {this.renderImg(other)}
            </View>
        );
    }
}