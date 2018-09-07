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

export default class ServiceCategories extends Component {

  constructor(props) {
    super(props);
    this.ds = new ListView.DataSource({ rowHasChanged: (r1, r2) => r1 !== r2 });
    this.state = {
      basic: true,
      listViewData: dataArray,
    };
  }

  render() {
    const ds = new ListView.DataSource({ rowHasChanged: (r1, r2) => r1 !== r2 });

    return (
      <Container>
        <Content>
          <List
            // leftOpenValue={75}
            rightOpenValue={-75}
            dataSource={this.ds.cloneWithRows(this.state.listViewData)}
            renderRow={this._renderRow}
            renderLeftHiddenRow={data =>
              <Button full onPress={() => alert(data)}>
                <Icon active name="information-circle" />
              </Button>}
            renderRightHiddenRow={(data, secId, rowId, rowMap) =>
              <Button full danger onPress={_ => this._deleteRow(secId, rowId, rowMap)}>
                <Icon active name="trash" />
              </Button>}
          />

        </Content>
      </Container>
    );
  }

  _renderRow(item) {
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
  }

  _deleteRow(secId, rowId, rowMap) {
    rowMap[`${secId}${rowId}`].props.closeRow();
    const newData = [...this.state.listViewData];
    newData.splice(rowId, 1);
    this.setState({ listViewData: newData });
  }
}
