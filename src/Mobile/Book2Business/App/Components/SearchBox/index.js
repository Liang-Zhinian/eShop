import React, { Component } from "react";
import {
    View,
    TextInput,
    TouchableWithoutFeedback,
    TouchableOpacity,
    StyleSheet,
    Dimensions,
    Text,
    Animated,
    Keyboard,
} from "react-native";
import PropTypes from 'prop-types'
import Icon from 'react-native-vector-icons/FontAwesome';

// import {translate} from '../../../../i18n/i18n';
function translate(text) { return text }

const AnimatedTextInput = Animated.createAnimatedComponent(TextInput);
const { width, height } = Dimensions.get('window');
const containerHeight = 40;

class SearchBox extends Component {

    static propTypes = {
        onBeforeSearch: PropTypes.func,
        onSearch: PropTypes.func.isRequired,
        onAfterSearch: PropTypes.func,
    }

    static defaultProps = {
        editable: true,
        blurOnSubmit: true,
        keyboardShouldPersist: false,
        searchIconCollapsedMargin: 25,
        searchIconExpandedMargin: 10,
        placeholderCollapsedMargin: 15,
        placeholderExpandedMargin: 20,
        shadowOffsetWidth: 0,
        shadowOffsetHeightCollapsed: 2,
        shadowOffsetHeightExpanded: 4,
        shadowColor: '#000',
        shadowOpacityCollapsed: 0.12,
        shadowOpacityExpanded: 0.24,
        shadowRadius: 4,
        shadowVisible: false,
        useClearButton: true,
    }

    constructor(props) {
        super(props)
        this.state = {
            keyword: '',
            expanded: false,
        }


        const { width } = Dimensions.get('window');
        this.contentWidth = width;
        this.middleWidth = width / 2;
        this.cancelButtonWidth = this.props.cancelButtonWidth || 70;

        /**
         * Animated values
         */
        this.iconSearchAnimated = new Animated.Value(
            this.middleWidth - this.props.searchIconCollapsedMargin
        );
        this.iconDeleteAnimated = new Animated.Value(0);
        this.inputFocusWidthAnimated = new Animated.Value(this.contentWidth - 10);
        this.inputFocusPlaceholderAnimated = new Animated.Value(
            this.middleWidth - this.props.placeholderCollapsedMargin
        );
        this.btnCancelAnimated = new Animated.Value(this.contentWidth);

        /**
         * Shadow
         */
        this.shadowOpacityAnimated = new Animated.Value(
            this.props.shadowOpacityCollapsed
        );
        this.shadowHeight = this.props.shadowOffsetHeightCollapsed;
    }

    render() {
        const styles = getStyles(this.props.inputHeight);
        return (
            <Animated.View
                ref="searchContainer"
                style={[
                    styles.container,
                    this.props.backgroundColor && {
                        backgroundColor: this.props.backgroundColor
                    }
                ]}
                onLayout={this.onLayout}
            >
                <AnimatedTextInput
                    ref="input_keyword"
                    style={[
                        styles.input,
                        this.props.placeholderTextColor && {
                            color: this.props.placeholderTextColor
                        },
                        this.props.inputStyle && this.props.inputStyle,
                        this.props.inputHeight && { height: this.props.inputHeight },
                        this.props.inputBorderRadius && {
                            borderRadius: this.props.inputBorderRadius
                        },
                        {
                            width: this.inputFocusWidthAnimated,
                            paddingLeft: this.inputFocusPlaceholderAnimated
                        },
                        this.props.shadowVisible && {
                            shadowOffset: {
                                width: this.props.shadowOffsetWidth,
                                height: this.shadowHeight
                            },
                            shadowColor: this.props.shadowColor,
                            shadowOpacity: this.shadowOpacityAnimated,
                            shadowRadius: this.props.shadowRadius
                        }
                    ]}
                    editable={this.props.editable}
                    value={this.state.keyword}
                    onChangeText={this.onChangeText}
                    placeholder={this.props.placeholder || translate('Search')}
                    placeholderTextColor={
                        this.props.placeholderTextColor || styles.placeholderColor
                    }
                    selectionColor={this.props.selectionColor}
                    onSubmitEditing={this.onSearch}
                    autoCorrect={false}
                    blurOnSubmit={this.props.blurOnSubmit}
                    returnKeyType={this.props.returnKeyType || 'search'}
                    keyboardType={this.props.keyboardType || 'default'}
                    autoCapitalize={this.props.autoCapitalize}
                    onFocus={this.onFocus}
                    underlineColorAndroid="transparent"
                />

                <TouchableWithoutFeedback onPress={this.onFocus}>
                    <Animated.View
                        style={[styles.iconSearch, { left: this.iconSearchAnimated }]}
                    >

                        <Icon
                            name="search"
                            color="grey"
                            size={14}
                            style={{
                                backgroundColor: 'transparent',
                                color: 'grey',
                            }}
                        />
                    </Animated.View>
                </TouchableWithoutFeedback>

                <TouchableWithoutFeedback onPress={this.onDelete}>

                    <Animated.View
                        style={[
                            styles.iconDelete,
                            styles.iconDeleteDefault,
                            this.props.tintColorDelete && {
                                tintColor: this.props.tintColorDelete
                            },
                            this.props.positionRightDelete && {
                                right: this.props.positionRightDelete
                            },
                            { opacity: this.iconDeleteAnimated }
                        ]}
                    >
                        <Icon
                            name="times-circle"
                            color="grey"
                            size={14}
                            style={{
                                backgroundColor: 'transparent',
                                color: 'grey',
                            }}
                        />
                    </Animated.View>
                </TouchableWithoutFeedback>

                <TouchableOpacity onPress={this.onCancel}>
                    <Animated.View
                        style={[
                            styles.cancelButton,
                            this.props.cancelButtonStyle && this.props.cancelButtonStyle,
                            this.props.cancelButtonViewStyle && this.props.cancelButtonViewStyle,
                            { left: this.btnCancelAnimated }
                        ]}
                    >
                        <Text
                            style={[
                                styles.cancelButtonText,
                                this.props.titleCancelColor && {
                                    color: this.props.titleCancelColor
                                },
                                this.props.cancelButtonStyle && this.props.cancelButtonStyle,
                                this.props.cancelButtonTextStyle && this.props.cancelButtonTextStyle,
                            ]}
                        >
                            Cancel
                        </Text>
                    </Animated.View>
                </TouchableOpacity>
            </Animated.View>
        );
    } // render

    /**
       * onDelete
       * async await
       */
    onDelete = async () => {
        this.props.beforeDelete && (await this.props.beforeDelete());
        await new Promise((resolve, reject) => {
            Animated.timing(this.iconDeleteAnimated, {
                toValue: 0,
                duration: 200
            }).start(() => resolve());
        });
        await this.setState({ keyword: '' });
        this.props.onDelete && (await this.props.onDelete());
        this.props.afterDelete && (await this.props.afterDelete());
    };

    onLayout = event => {
        // debugger;
        const contentWidth = event.nativeEvent.layout.width;
        this.contentWidth = contentWidth;
        this.middleWidth = contentWidth / 2;
        if (this.state.expanded) {
            this.expandAnimation();
        } else {
            this.collapseAnimation();
        }
    }

    onFocus = async () => {
        this.props.beforeFocus && (await this.props.beforeFocus());
        this.refs.input_keyword._component.isFocused &&
            (await this.refs.input_keyword._component.focus());
        await this.setState(prevState => {
            return { expanded: !prevState.expanded };
        });
        await this.expandAnimation();
        this.props.onFocus && (await this.props.onFocus(this.state.keyword));
        this.props.afterFocus && (await this.props.afterFocus());
    }

    /**
       * focus
       * async await
       */
    focus = async (text = '') => {
        await this.setState({ keyword: text });
        await this.refs.input_keyword._component.focus();
    };

    /**
     * onSearch
     * async await
     */
    onSearch = async () => {
        this.props.onBeforeSearch &&
            (await this.props.onBeforeSearch(this.state.keyword));
        if (this.props.keyboardShouldPersist === false) {
            await Keyboard.dismiss();
        }
        this.props.onSearch && (await this.props.onSearch(this.state.keyword));
        this.props.onAfterSearch &&
            (await this.props.onAfterSearch(this.state.keyword));
    };
    /**
   * onChangeText
   * async await
   */
    onChangeText = async text => {
        await this.setState({ keyword: text });
        await new Promise((resolve, reject) => {
            Animated.timing(this.iconDeleteAnimated, {
                toValue: text.length > 0 ? 1 : 0,
                duration: 200
            }).start(() => resolve());
        });
        this.props.onChangeText &&
            (await this.props.onChangeText(this.state.keyword));
    };

    /**
       * onCancel
       * async await
       */
    onCancel = async () => {
        this.props.beforeCancel && (await this.props.beforeCancel());
        await this.setState({ keyword: '' });
        await this.setState(prevState => {
            return { expanded: !prevState.expanded };
        });
        await this.collapseAnimation(true);
        this.props.onCancel && (await this.props.onCancel());
        this.props.afterCancel && (await this.props.afterCancel());
    };

    expandAnimation = () => {
        return new Promise((resolve, reject) => {
            Animated.parallel([
                Animated.timing(this.inputFocusWidthAnimated, {
                    toValue: this.contentWidth - this.cancelButtonWidth,
                    duration: 200
                }).start(),
                Animated.timing(this.btnCancelAnimated, {
                    toValue: 10,
                    duration: 200
                }).start(),
                Animated.timing(this.inputFocusPlaceholderAnimated, {
                    toValue: this.props.placeholderExpandedMargin,
                    duration: 200
                }).start(),
                Animated.timing(this.iconSearchAnimated, {
                    toValue: this.props.searchIconExpandedMargin,
                    duration: 200
                }).start(),
                Animated.timing(this.iconDeleteAnimated, {
                    toValue: this.state.keyword.length > 0 ? 1 : 0,
                    duration: 200
                }).start(),
                Animated.timing(this.shadowOpacityAnimated, {
                    toValue: this.props.shadowOpacityExpanded,
                    duration: 200
                }).start()
            ]);
            this.shadowHeight = this.props.shadowOffsetHeightExpanded;
            resolve();
        });
    };

    collapseAnimation = (isForceAnim = false) => {
        return new Promise((resolve, reject) => {
            Animated.parallel([
                this.props.keyboardShouldPersist === false ? Keyboard.dismiss() : null,
                Animated.timing(this.inputFocusWidthAnimated, {
                    toValue: this.contentWidth - 10,
                    duration: 200
                }).start(),
                Animated.timing(this.btnCancelAnimated, {
                    toValue: this.contentWidth,
                    duration: 200
                }).start(),
                this.props.keyboardShouldPersist === false
                    ? Animated.timing(this.inputFocusPlaceholderAnimated, {
                        toValue: this.middleWidth - this.props.placeholderCollapsedMargin,
                        duration: 200
                    }).start()
                    : null,
                this.props.keyboardShouldPersist === false || isForceAnim === true
                    ? Animated.timing(this.iconSearchAnimated, {
                        toValue: this.middleWidth - this.props.searchIconCollapsedMargin,
                        duration: 200
                    }).start()
                    : null,
                Animated.timing(this.iconDeleteAnimated, {
                    toValue: 0,
                    duration: 200
                }).start(),
                Animated.timing(this.shadowOpacityAnimated, {
                    toValue: this.props.shadowOpacityCollapsed,
                    duration: 200
                }).start()
            ]);
            this.shadowHeight = this.props.shadowOffsetHeightCollapsed;
            resolve();
        });
    };


}

export default SearchBox;


const getStyles = (inputHeight) => {
    let middleHeight = 20
    if (typeof inputHeight == 'number')
        middleHeight = (10 + inputHeight) / 2;

    return {
        container: {
            backgroundColor: 'grey',
            height: containerHeight,
            flexDirection: 'row',
            justifyContent: 'flex-start',
            alignItems: 'center',
            padding: 5
        },
        input: {
            height: containerHeight - 10,
            paddingTop: 5,
            paddingBottom: 5,
            paddingRight: 20,
            borderColor: '#444',
            backgroundColor: '#f7f7f7',
            borderRadius: 5,
            fontSize: 13
        },
        placeholderColor: 'grey',
        iconSearch: {
            flex: 1,
            position: 'absolute',
            top: middleHeight - 7,
            height: 14,
            width: 14,
        },
        iconSearchDefault: {
            // tintColor: 'grey'
        },
        iconDelete: {
            position: 'absolute',
            right: 70,
            top: middleHeight - 7,
            height: 14,
            width: 14
        },
        iconDeleteDefault: {
            // tintColor: 'grey'
        },
        cancelButton: {
            justifyContent: 'center',
            alignItems: 'flex-start',
            backgroundColor: 'transparent',
            width: 60,
            height: 50
        },
        cancelButtonText: {
            fontSize: 14,
            color: '#fff'
        }
    };
}
