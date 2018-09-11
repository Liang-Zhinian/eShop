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

class UpdateLocationInformation extends React.Component {
    static propTypes = {
        error: PropTypes.string,
        success: PropTypes.string,
        loading: PropTypes.bool,
        // onFormSubmit: PropTypes.func.isRequired,
        location: PropTypes.shape({
            name: PropTypes.string,
            description: PropTypes.string,
        }).isRequired,
    }

    static defaultProps = {
        error: null,
        success: null,
    }

    constructor(props) {
        super(props);
        this.state = {
            name: '',
            description: '',
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

    onFormSubmit = (data) => {
    //   const { onFormSubmit } = this.props;
    //   return onFormSubmit(data)
    //     .then(mes => this.setState({ successMessage: mes, errorMessage: null }))
    //     .catch((err) => { this.setState({ errorMessage: err, successMessage: null }); throw err; });
    }

    render() {
        const { loading, error, success } = this.props;

        console.log(this)
        const {
            name,
            description,
        } = this.state;

        // Loading
        if (loading) return <Loading />;

        return (
            <Container>
                <Content padder>
                    <Header
                        title="Business location"
                        content="Thanks for keeping your business location up to date!"
                    />

                    {error && <Messages message={error} />}
                    {success && <Messages message={success} type="success" />}

                    <Form>
                        <Item stackedLabel>
                            <Label>
                                Name
                </Label>
                            <Input
                                value={name}
                                onChangeText={v => this.handleChange('name', v)}
                            />
                        </Item>

                        <Item stackedLabel>
                            <Label>
                                Description
            </Label>
                            <Input
                                value={description}
                                onChangeText={v => this.handleChange('description', v)}
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

export default UpdateLocationInformation;

