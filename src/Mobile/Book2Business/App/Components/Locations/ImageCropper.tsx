import React, { Component } from 'react';
import {
  View, Text, StyleSheet, ScrollView, Alert,
  Image, TouchableOpacity, NativeModules, Dimensions
} from 'react-native';
import {
  Left, Right, Body, Button, Title
} from 'native-base'
import ImagePicker from 'react-native-image-crop-picker';
import { Images, Colors } from '../../Themes'
import styles from '../Styles/ImageCropperStyles'
// import Icon from 'react-native-vector-icons/FontAwesome'
// import AnimatedButton from '../AnimatedButton'
import GradientView from '../GradientView'
import GradientHeader from '../GradientHeader'
import MainContainer from '../../Containers/MainContainer'

// var ImagePicker = NativeModules.ImageCropPicker;


interface ImageCropperProps {
  screenProps: { toggle(): void }
  style: StyleSheet
  isLoading: boolean
  showError(err): object
  handePickButton(item): void
}

interface ImageCropperState {
  isLoading: boolean
  image: object,
  images: object[]
}

export default class ImageCropper extends Component<ImageCropperProps, ImageCropperState> {

  constructor(props) {
    super(props);
    this.state = {
      isLoading: false,
      image: null,
      images: null
    };
  }

  componentDidMount() {
    // this.pickSingle(true)
  }

  pickSingleWithCamera(cropping) {
    ImagePicker.openCamera({
      cropping: cropping,
      width: 500,
      height: 500,
      includeExif: true,
    }).then(image => {
      console.log('received image', image);
      this.setState({
        image: { uri: image.path, width: image.width, height: image.height },
        images: null
      });
    }).catch(e => alert(e));
  }

  pickSingleBase64(cropit) {
    ImagePicker.openPicker({
      width: 300,
      height: 300,
      cropping: cropit,
      includeBase64: true,
      includeExif: true,
    }).then(image => {
      console.log('received base64 image');
      this.setState({
        image: { uri: `data:${image.mime};base64,` + image.data, width: image.width, height: image.height },
        images: null
      });
    }).catch(e => alert(e));
  }

  cleanupImages() {
    ImagePicker.clean().then(() => {
      console.log('removed tmp images from tmp directory');
    }).catch(e => {
      alert(e);
    });
  }

  cleanupSingleImage() {
    let image = this.state.image || (this.state.images && this.state.images.length ? this.state.images[0] : null);
    console.log('will cleanup image', image);

    ImagePicker.cleanSingle(image ? image.uri : null).then(() => {
      console.log(`removed tmp image ${image.uri} from tmp directory`);
    }).catch(e => {
      alert(e);
    })
  }

  cropLast() {
    if (!this.state.image) {
      return Alert.alert('No image', 'Before open cropping only, please select image');
    }

    ImagePicker.openCropper({
      path: this.state.image.uri,
      width: 200,
      height: 200
    }).then(image => {
      console.log('received cropped image', image);
      this.setState({
        image: { uri: image.path, width: image.width, height: image.height, mime: image.mime },
        images: null
      });
    }).catch(e => {
      console.log(e);
      Alert.alert(e.message ? e.message : e);
    });
  }

  pickSingle(cropit, circular = false) {
    ImagePicker.openPicker({
      width: 300,
      height: 200,
      cropping: cropit,
      cropperCircleOverlay: circular,
      compressImageMaxWidth: 640,
      compressImageMaxHeight: 480,
      compressImageQuality: 0.5,
      compressVideoPreset: 'MediumQuality',
      includeExif: true,
    }).then(image => {
      console.log('received image', image);
      this.setState({
        image: { uri: image.path, width: image.width, height: image.height, mime: image.mime },
        images: null
      });
    }).catch(e => {
      console.log(e);
      Alert.alert(e.message ? e.message : e);
    });
  }

  pickMultiple() {
    ImagePicker.openPicker({
      multiple: true,
      waitAnimationEnd: false,
      includeExif: true,
      forceJpg: true,
    }).then(images => {
      this.setState({
        image: null,
        images: images.map(i => {
          console.log('received image', i);
          return { uri: i.path, width: i.width, height: i.height, mime: i.mime };
        })
      });
    }).catch(e => alert(e));
  }

  scaledHeight(oldW, oldH, newW) {
    return (oldH / oldW) * newW;
  }

  renderImage(image) {
    return <Image style={{ width: 300, height: 300, resizeMode: 'contain' }} source={image} />
  }

  renderAsset(image) {

    return this.renderImage(image);
  }

  renderHeader() {
    return (

      <GradientHeader>
        <View style={[styles.header]}>
          <Left>
            <TouchableOpacity style={styles.backButton} onPress={this.props.screenProps.toggle}>
              <Image style={styles.backButtonIcon} source={Images.arrowIcon} />
            </TouchableOpacity>
          </Left>
          <Body>
            <Title style={{ color: Colors.snow }}>Pick a location</Title>
          </Body>
          <Right>
            <Button transparent onPress={() => {
              this.props.screenProps.toggle()
            }}>
              <Title style={{ color: Colors.snow }}>Done</Title>
            </Button>
          </Right>
        </View>
      </GradientHeader>
    )
  }

  render() {
    return (
      <MainContainer
        header={{
          headerTitle: 'Pick an image',
          goBack: this.props.screenProps.toggle,
          headerRight: (
            <Button transparent onPress={() => {
              this.props.screenProps.toggle();
              this.props.handePickButton(this.state.image);
            }}>
              <Title style={{ color: Colors.snow }}>Done</Title>
            </Button>
          )
        }}>
        <View style={{}}>
          <ScrollView>
            {this.state.image ? this.renderAsset(this.state.image) : null}
            {this.state.images ? this.state.images.map(i => <View key={i.uri}>{this.renderAsset(i)}</View>) : null}
          </ScrollView>


          <TouchableOpacity onPress={() => this.pickSingle(true)} style={styles.button}>
            <Text style={styles.text}>
              {
                this.state.image ? 'Re-select' : 'Select an image'
              }</Text>
          </TouchableOpacity>

        </View>

        {/* <TouchableOpacity onPress={() => this.pickSingleWithCamera(false)} style={styles.button}>
            <Text style={styles.text}>Select Single With Camera</Text>
          </TouchableOpacity>
          <TouchableOpacity onPress={() => this.pickSingleWithCamera(true)} style={styles.button}>
            <Text style={styles.text}>Select Single With Camera With Cropping</Text>
          </TouchableOpacity>
          <TouchableOpacity onPress={() => this.pickSingle(false)} style={styles.button}>
            <Text style={styles.text}>Select Single</Text>
          </TouchableOpacity>
          <TouchableOpacity onPress={() => this.cropLast()} style={styles.button}>
            <Text style={styles.text}>Crop Last Selected Image</Text>
          </TouchableOpacity>
          <TouchableOpacity onPress={() => this.pickSingleBase64(false)} style={styles.button}>
            <Text style={styles.text}>Select Single Returning Base64</Text>
          </TouchableOpacity>
          <TouchableOpacity onPress={() => this.pickSingle(true)} style={styles.button}>
            <Text style={styles.text}>Select Single With Cropping</Text>
          </TouchableOpacity>
          <TouchableOpacity onPress={() => this.pickSingle(true, true)} style={styles.button}>
            <Text style={styles.text}>Select Single With Circular Cropping</Text>
          </TouchableOpacity>
          <TouchableOpacity onPress={this.pickMultiple.bind(this)} style={styles.button}>
            <Text style={styles.text}>Select Multiple</Text>
          </TouchableOpacity>
          <TouchableOpacity onPress={this.cleanupImages.bind(this)} style={styles.button}>
            <Text style={styles.text}>Cleanup All Images</Text>
          </TouchableOpacity>
          <TouchableOpacity onPress={this.cleanupSingleImage.bind(this)} style={styles.button}>
            <Text style={styles.text}>Cleanup Single Image</Text>
          </TouchableOpacity> */}
      </MainContainer>
    );
  }
}