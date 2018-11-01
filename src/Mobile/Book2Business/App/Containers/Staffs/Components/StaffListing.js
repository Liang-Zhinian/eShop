import React from 'react'

import Staff from './Staff'
import styles from '../Styles/StaffsScreenStyle'
import List from '../../../Components/List'

export default ({
  loading,
  error,
  data,
  reFetch,
  renderRow,
  navigation, 
  setSelectedStaff
}) => {
  console.log('stafflisting', {
    loading,
    error,
    data,
    reFetch,
    renderRow,
    navigation, 
    setSelectedStaff
  })

  const onEventPress = (item) => {
    setSelectedStaff(item)

    navigation.navigate('Staff')
  }

  const renderItem = renderRow || function ({ item }) {
    console.log(item)
    return (
      <Staff
        type={'talk'}
        name={item.FirstName + ' ' + item.LastName}
        avatarURL={item.ImageUri || ''}
        title={item.Bio}
        onPress={() => onEventPress(item)}
      />
    )
  }

  const keyExtractor = (item, idx) => item.Id

  return (
    <List
      headerTitle='Staffs'
      navigation={navigation}
      data={data}
      renderItem={renderItem}
      keyExtractor={keyExtractor}
      contentContainerStyle={styles.listContent}
      showsVerticalScrollIndicator={false}
      reFetch={reFetch}
      error={error}
      loading={loading}
    />
  )
}
