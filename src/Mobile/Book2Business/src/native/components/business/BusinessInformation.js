import React from 'react';
import PropTypes from 'prop-types';
import { View } from 'react-native';
import {
    Container, Content, List, ListItem, Body, Left, Text, Icon,
} from 'native-base';
import { Actions } from 'react-native-router-flux';
import Header from '../Header';

const BusinessInformation = ({ member, logout }) => (
    <Container>
        <Content>
            <List>

                <ListItem onPress={Actions.location} icon>
                    <Left>
                        <Icon name="pin" />
                    </Left>
                    <Body>
                        <Text>
                            Business location
                        </Text>
                    </Body>
                </ListItem>

                <ListItem onPress={Actions.contactInformation} icon>
                    <Left>
                        <Icon name="contacts" />
                    </Left>
                    <Body>
                        <Text>
                            Contact information
                        </Text>
                    </Body>
                </ListItem>

            </List>
        </Content>
    </Container>
);

BusinessInformation.propTypes = {
    member: PropTypes.shape({}),
    logout: PropTypes.func,
};

BusinessInformation.defaultProps = {
    member: {},
};

export default BusinessInformation;
