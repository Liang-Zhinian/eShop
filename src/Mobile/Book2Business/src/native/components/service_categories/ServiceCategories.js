import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { ListView, TouchableOpacity } from 'react-native';
import { Container, Header, Content, Left, Body, Right, Button, Icon, List, ListItem, Text } from 'native-base';

const dataArray = [
    { title: "Single", content: "Lorem ipsum dolor sit amet" },
    { title: "Complimentary", content: "Lorem ipsum dolor sit amet" },
    { title: "Redeemable", content: "Lorem ipsum dolor sit amet" },
    { title: "Package", content: "Lorem ipsum dolor sit amet" },
    { title: "Personal Training", content: "Lorem ipsum dolor sit amet" }
];

const ServiceCategoryListing = ({
    error,
    loading,
    serviceCategories,
    reFetch,
}) => {

    // Loading
    if (loading) return <Loading />;

    // Error
    if (error) return <Error content={error} />;

    const keyExtractor = item => item.id;

    //   const onPress = item => Actions.recipe({ match: { params: { id: String(item.id) } } });

    const ds = new ListView.DataSource({ rowHasChanged: (r1, r2) => r1 !== r2 });

    console.log('ServiceCategoryListing', serviceCategories);

    return (
        <Container>
            <Content>
                <List
                    // leftOpenValue={75}
                    rightOpenValue={-75}
                    dataSource={ds.cloneWithRows(serviceCategories)}
                    renderRow={(item) => {
                        return <ListItem thumbnail>
                            <Body>
                                <Text>{item.title}</Text>
                                <Text note numberOfLines={1}>{item.content}</Text>
                            </Body>
                            <Right>
                                <Button transparent>
                                    <Text>Rename</Text>
                                </Button>
                            </Right>
                        </ListItem>
                    }}
                    renderLeftHiddenRow={data =>
                        <Button full onPress={() => alert(data)}>
                            <Icon active name="information-circle" />
                        </Button>}
                    renderRightHiddenRow={(data, secId, rowId, rowMap) =>
                        <Button full danger onPress={_ => {
                            rowMap[`${secId}${rowId}`].props.closeRow();
                            const newData = [...this.state.listViewData];
                            newData.splice(rowId, 1);
                            this.setState({ listViewData: newData });
                        }}>
                            <Icon active name="trash" />
                        </Button>}
                />

            </Content>
        </Container>
    );
};

ServiceCategoryListing.propTypes = {
    error: PropTypes.string,
    loading: PropTypes.bool.isRequired,
    serviceCategories: PropTypes.arrayOf(PropTypes.shape()).isRequired,
    reFetch: PropTypes.func,
};

ServiceCategoryListing.defaultProps = {
    error: null,
    reFetch: null,
    ds: new ListView.DataSource({ rowHasChanged: (r1, r2) => r1 !== r2 }),
    renderRow: (item) => {
        return <ListItem thumbnail>
            <Body>
                <Text>{item.title}</Text>
                <Text note numberOfLines={1}>{item.content}</Text>
            </Body>
            <Right>
                <Button transparent>
                    <Text>Rename</Text>
                </Button>
            </Right>
        </ListItem>
    },
    deleteRow: (secId, rowId, rowMap) => {
        rowMap[`${secId}${rowId}`].props.closeRow();
        const newData = [...this.state.listViewData];
        newData.splice(rowId, 1);
        this.setState({ listViewData: newData });
    }
};

export default ServiceCategoryListing;


