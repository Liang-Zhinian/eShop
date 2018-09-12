import React from 'react';
import PropTypes from 'prop-types';
import {
    Container, Content, Text, Body, ListItem, Form, Item, Label, Input, CheckBox, Button, View,
} from 'native-base';
import { connect } from 'react-redux';
import Messages from '../Messages';
import Loading from '../Loading';
import Header from '../Header';
import Spacer from '../Spacer';

import site from '../../../constants/site';
import { getLocation } from '../../../actions/locations';

class UpdateGeolocation extends React.Component {
    static propTypes = {
        error: PropTypes.string,
        success: PropTypes.string,
        loading: PropTypes.bool,
        // onFormSubmit: PropTypes.func.isRequired,
        location: PropTypes.shape({
            latitude: PropTypes.string,
            longitude: PropTypes.string,
        }).isRequired,
    }

    static defaultProps = {
        error: null,
        success: null,
    }

    constructor(props) {
        super(props);
        this.state = {
            latitude: props.location.Geolocation.Latitude,
            longitude: props.location.Geolocation.Longitude,
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);

        // this.init();
    }

    init = () => {
        const {siteId, locationId} = site;

        this.props.getLocation(siteId, locationId)
        .then(res=>{
            // console.log('init', res)

            // console.log(this)
        })
    }

    handleChange = (name, val) => {
        this.setState({
            [name]: val,
        });
    }

    handleSubmit = () => {
        const { onFormSubmit } = this.props;
        onFormSubmit(this.state)
            .then(() => console.log('Location Updated'))
            .catch(e => console.log(`Error: ${e}`));
    }

    render() {
        const { loading, error, success } = this.props;

        const {
            latitude,
            longitude,
        } = this.state;

        // Loading
        if (loading) return <Loading />;

        return (
            <Container>
                <Content padder>
                    <Header
                        title="Geolocation"
                        content="Thanks for keeping your geolocation up to date!"
                    />

                    {error && <Messages message={error} />}
                    {success && <Messages message={success} type="success" />}

                    <Form>
                    <Item stackedLabel>
                            <Label>
                                Latitude
                </Label>
                            <Input
                                value={latitude}
                                onChangeText={v => this.handleChange('latitude', v)}
                            />
                        </Item>

                        <Item stackedLabel>
                            <Label>
                                Longitude
            </Label>
                            <Input
                                value={longitude}
                                onChangeText={v => this.handleChange('longitude', v)}
                            />
                        </Item>

                        <Spacer size={20} />

                        <Button block onPress={this.handleSubmit}>
                            <Text>
                                Save
              </Text>
                        </Button>
                    </Form>
                </Content>
            </Container>
        );
    }
}

export default UpdateGeolocation;

