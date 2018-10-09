const ApiKey = 'YPPBZ-KWYHI-PD3G2-5326X-SFSI2-3RFPH'
const PROTOCOL = 'http:'
const HOST = 'apis.map.qq.com'
const Api = `${PROTOCOL}//${HOST}/ws`
const ApiTranslate = `${Api}/coord/v1/translate?type=1&key=${ApiKey}`
const ApiCoordToAddress = `${Api}/geocoder/v1/?key=${ApiKey}`
// import handle from '../../../ExceptionHandler';

export const translate = (location) => {
  let url = `${ApiTranslate}&locations=${location.latitude},${location.longitude}`

  return fetch(url, { method: 'GET' })
        .then(response => response.json())
        .then(responseJson => {
          if (responseJson.status === 0) {
            return {
              latitude: responseJson.locations[0].lat,
              longitude: responseJson.locations[0].lng
            }
          }

          throw new Error(`Error${responseJson.status}: ${responseJson.message}`)
        })
        .catch(error => console.error(error))
    /*
    {
        "status": 0,
        "message": "转换成功",
        "locations": [
            {
                "lng": 116.841797,
                "lat": 39.12175
            }
        ]
    }
    */
}

export const coordToAddress = (location) => {
  let url = `${ApiCoordToAddress}&get_poi=1&location=${location.latitude},${location.longitude}`
  return fetch(url, { method: 'GET' })
        .then(response => response.json())
        .then(responseJson => {
          if (responseJson.status === 0) {
            var { nation, province, city, district } = responseJson.result.address_component
            var { recommend } = responseJson.result.formatted_addresses
            var address = `${nation} ${province} ${city} ${district} ${recommend || ''}`
            return {
              address,
              details: responseJson.result
            }
          }

          var error = new Error(`Error${responseJson.status}: ${responseJson.message}`)
            // handle(error);
          throw error
        })
        .catch(error => {
            // handle(error);
            // console.error(error)
          throw error
        })
    /*
    {
    "status": 0,
    "message": "query ok",
    "request_id": "57759a5e-db2a-11e7-9358-246e965de502",
    "result": {
        "location": {
            "lat": 39.12088,
            "lng": 116.835904
        },
        "address": "河北省廊坊市霸州市",
        "formatted_addresses": {
            "recommend": "霸州市杨芬港镇刘家堡",
            "rough": "霸州市杨芬港镇刘家堡"
        },
        "address_component": {
            "nation": "中国",
            "province": "河北省",
            "city": "廊坊市",
            "district": "霸州市",
            "street": "",
            "street_number": ""
        },
        "ad_info": {
            "nation_code": "156",
            "adcode": "131081",
            "city_code": "156131000",
            "name": "中国,河北省,廊坊市,霸州市",
            "location": {
                "lat": 39.12088,
                "lng": 116.835907
            },
            "nation": "中国",
            "province": "河北省",
            "city": "廊坊市",
            "district": "霸州市"
        },
        "address_reference": {
            "town": {
                "title": "杨芬港镇",
                "location": {
                    "lat": 39.12088,
                    "lng": 116.835907
                },
                "_distance": 0,
                "_dir_desc": "内"
            },
            "landmark_l2": {
                "title": "刘家堡",
                "location": {
                    "lat": 39.125359,
                    "lng": 116.8349
                },
                "_distance": 506.1,
                "_dir_desc": "南"
            }
        }
    }
}
    */
}

export const getNearby = (location, pageSize = 20, pageIndex = 1) => {
  if (pageIndex <= 0) pageIndex = 1

  let url = `${ApiCoordToAddress}&get_poi=1&location=${location.latitude},${location.longitude}&poi_options=radius=5000;page_size=${pageSize};page_index=${pageIndex};policy=2`
  return fetch(url, { method: 'GET' })
        .then(response => response.json())
        .then(responseJson => {
          if (responseJson.status === 0) {
            return responseJson.result.pois.map(poi => ({
              id: poi.id,
              title: poi.title,
              address: poi.address,
              location: {
                latitude: poi.location.lat,
                longitude: poi.location.lng
              }
            }))
          }

          var error = new Error(`Error${responseJson.status}: ${responseJson.message}`)
            // handle(error);
          throw error
        })
        .catch(error => {
            // handle(error);
            // console.error(error)
          throw error
        })
    /*
    {
    "status": 0,
    "message": "query ok",
    "request_id": "57759a5e-db2a-11e7-9358-246e965de502",
    "result": {
        "location": {
            "lat": 39.12088,
            "lng": 116.835904
        },
        "address": "河北省廊坊市霸州市",
        "formatted_addresses": {
            "recommend": "霸州市杨芬港镇刘家堡",
            "rough": "霸州市杨芬港镇刘家堡"
        },
        "address_component": {
            "nation": "中国",
            "province": "河北省",
            "city": "廊坊市",
            "district": "霸州市",
            "street": "",
            "street_number": ""
        },
        "ad_info": {
            "nation_code": "156",
            "adcode": "131081",
            "city_code": "156131000",
            "name": "中国,河北省,廊坊市,霸州市",
            "location": {
                "lat": 39.12088,
                "lng": 116.835907
            },
            "nation": "中国",
            "province": "河北省",
            "city": "廊坊市",
            "district": "霸州市"
        },
        "address_reference": {
            "town": {
                "title": "杨芬港镇",
                "location": {
                    "lat": 39.12088,
                    "lng": 116.835907
                },
                "_distance": 0,
                "_dir_desc": "内"
            },
            "landmark_l2": {
                "title": "刘家堡",
                "location": {
                    "lat": 39.125359,
                    "lng": 116.8349
                },
                "_distance": 506.1,
                "_dir_desc": "南"
            }
        }
    }
}
    */
}
