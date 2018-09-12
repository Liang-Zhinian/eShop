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

class UpdateAddress extends React.Component {
    static propTypes = {
        error: PropTypes.string,
        success: PropTypes.string,
        loading: PropTypes.bool,
        // onFormSubmit: PropTypes.func.isRequired,
        location: PropTypes.shape({
            streetAddress: PropTypes.string,
            city: PropTypes.string,
            stateProvince: PropTypes.string,
            postalCode: PropTypes.string,
            country: PropTypes.string,
        }).isRequired,
    }

    static defaultProps = {
        error: null,
        success: null,
    }

    constructor(props) {
        super(props);
        this.state = {
            streetAddress: props.location.Address.Street,
            city: props.location.Address.City,
            stateProvince: props.location.Address.StateProvince,
            postalCode: props.location.Address.ZipCode,
            country: props.location.Address.Country,
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
            streetAddress,
            city,
            stateProvince,
            postalCode,
            country,
        } = this.state;

        // Loading
        if (loading) return <Loading />;

        return (
            <Container>
                <Content padder>
                    <Header
                        title="Address"
                        content="Thanks for keeping your address up to date!"
                    />

                    {error && <Messages message={error} />}
                    {success && <Messages message={success} type="success" />}

                    <Form>
                        <Item stackedLabel>
                            <Label>
                                Street address
                            </Label>
                            <Input
                                value={streetAddress}
                                onChangeText={v => this.handleChange('streetAddress', v)}
                            />
                        </Item>

                        <Item stackedLabel>
                            <Label>
                                City
                            </Label>
                            <Input
                                value={city}
                                onChangeText={v => this.handleChange('city', v)}
                            />
                        </Item>

                        <Item stackedLabel>
                            <Label>
                                State/Province
                            </Label>
                            <Input
                                value={stateProvince}
                                onChangeText={v => this.handleChange('stateProvince', v)}
                            />
                        </Item>

                        <Item stackedLabel>
                            <Label>
                                Postal code
                            </Label>
                            <Input
                                value={postalCode}
                                onChangeText={v => this.handleChange('postalCode', v)}
                            />
                        </Item>

                        <Item stackedLabel>
                            <Label>
                                Country
                            </Label>
                            <Input
                                value={country}
                                onChangeText={v => this.handleChange('country', v)}
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

export default UpdateAddress;

