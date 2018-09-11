import React from 'react';
import PropTypes from 'prop-types';
import { View } from 'react-native';
import {
    Container, Content, List, ListItem, Body, Left, Text, Icon,
} from 'native-base';
import { Actions } from 'react-native-router-flux';
import Header from './Header';

const More = ({ member, logout }) => (
    <Container>
        <Content>
            <List>

                <ListItem onPress={Actions.appointment_categories} icon>
                    <Left>
                        <Icon name="ios-flag" />
                    </Left>
                    <Body>
                        <Text>
                            Appointments
                        </Text>
                    </Body>
                </ListItem>

                <ListItem onPress={Actions.classes} icon>
                    <Left>
                        <Icon name="ios-flag" />
                    </Left>
                    <Body>
                        <Text>
                            Classes
                        </Text>
                    </Body>
                </ListItem>
                
                <ListItem onPress={Actions.locationMenus} icon>
                    <Left>
                        <Icon name="briefcase" />
                    </Left>
                    <Body>
                        <Text>
                            Business information
                        </Text>
                    </Body>
                </ListItem>

                <ListItem onPress={Actions.profile} icon>
                    <Left>
                        <Icon name="contact" />
                    </Left>
                    <Body>
                        <Text>
                            Profile
                        </Text>
                    </Body>
                </ListItem>

                <ListItem onPress={Actions.changeLocation} icon>
                    <Left>
                        <Icon name="pin" />
                    </Left>
                    <Body>
                        <Text>
                            Change location
                        </Text>
                    </Body>
                </ListItem>

            </List>
        </Content>
    </Container>
);

More.propTypes = {
    member: PropTypes.shape({}),
    logout: PropTypes.func,
};

More.defaultProps = {
    member: {},
};

export default More;
