import React from 'react'
import PropTypes from 'prop-types'
import {
  Container, Content, Text, H3
} from 'native-base'

const Error = ({ title, content, backgroundColor }) => (
  <Container style={{ backgroundColor: backgroundColor }}>
    <Content>
      <H3>
        {title}
      </H3>
      <Text>
        {content}
      </Text>
    </Content>
  </Container>
)

Error.propTypes = {
  title: PropTypes.string,
  content: PropTypes.string,
  backgroundColor: PropTypes.string
}

Error.defaultProps = {
  title: 'Uh oh',
  content: 'An unexpected error came up',
  backgroundColor: 'white'
}

export default Error
