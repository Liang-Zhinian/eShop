
export default {
  issuer: 'http://localhost:5105',
  clientId: 'native.code',
  redirectUrl: 'io.identityserver.book2:/oauthredirect',
  additionalParameters: {},
  scopes: ['openid', 'profile', 'role', 'mobilereservationagg']

    // serviceConfiguration: {
    //   authorizationEndpoint: 'https://demo.identityserver.io/connect/authorize',
    //   tokenEndpoint: 'https://demo.identityserver.io/connect/token',
    //   revocationEndpoint: 'https://demo.identityserver.io/connect/revoke'
    // }
}
