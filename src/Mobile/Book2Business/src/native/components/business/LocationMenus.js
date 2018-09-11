import React from 'react';
import PropTypes from 'prop-types';
import { View } from 'react-native';
import {
    Container, Content, List, ListItem, Body, Left, Text, Icon,
} from 'native-base';
import { Actions } from 'react-native-router-flux';
import Header from '../Header';

const LocationMenus = ({ member, logout }) => (
    <Container>
        <Content>
            <List>

                <ListItem onPress={Actions.locationInfo} icon>
                    <Left>
                        <Icon name="pin" />
                    </Left>
                    <Body>
                        <Text>
                            Location information
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

                <ListItem onPress={Actions.contactInformation} icon>
                    <Left>
                        <Icon name="contacts" />
                    </Left>
                    <Body>
                        <Text>
                            Geolocation
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

                <ListItem onPress={Actions.contactInformation} icon>
                    <Left>
                        <Icon name="contacts" />
                    </Left>
                    <Body>
                        <Text>
                            Address
                        </Text>
                    </Body>
                </ListItem>

                <ListItem onPress={Actions.contactInformation} icon>
                    <Left>
                        <Icon name="contacts" />
                    </Left>
                    <Body>
                        <Text>
                            Logo
                        </Text>
                    </Body>
                </ListItem>

                <ListItem onPress={Actions.contactInformation} icon>
                    <Left>
                        <Icon name="contacts" />
                    </Left>
                    <Body>
                        <Text>
                            Additional images
                        </Text>
                    </Body>
                </ListItem>

            </List>
        </Content>
    </Container>
);

LocationMenus.propTypes = {
    member: PropTypes.shape({}),
    logout: PropTypes.func,
};

LocationMenus.defaultProps = {
    member: {},
};

export default LocationMenus;
