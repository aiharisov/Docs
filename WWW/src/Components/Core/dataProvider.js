import dateformat from 'dateformat'
import Extension from './Extension.js'
import APIClient, * as URLs from './APIClient.js';
class dataProvider
{
    constructor(props){
        console.log('Init dataProvider component');
        this.client = new APIClient(props);
        console.log('end');
    }
    get Client()
    {
      return this.client;
    }
    LoadHeatMap(params)
    {
        console.log('dataProvider LoadHeatMap begin');
        let url = `${URLs.URLHeatMap}?type=${params.typeOfShip}`;
        console.log(url);
        return this.client.ApiGet(url).then(response => response.data);
    }
    LoadPaths(params)
    {
        console.log('dataProvider LoadPaths begin');
        console.log(URLs.URLPaths);
        return this.client.ApiGet(URLs.URLPaths).then(response => response.data);
    }
    LoadAvalibleShipTypes()
    {
        console.log('dataProvider LoadAvalibleShipTypes begin');
        console.log(URLs.URLShipTypes);
        return this.client.ApiGet(URLs.URLShipTypes).then(response => response.data);
    }
    LoadRoutes(params)
    {
        console.log('dataProvider LoadRoutes begin');
        console.log(params);
        var mmsi, start, end, typeOfShip;
        try {
            start = Extension.ConvertToDate(params.start);
            end = Extension.ConvertToDate(params.end);
        } finally {
            if ((!start || isNaN(start)) && (!end || isNaN(end))) {
                start = new Date();
                start.setMinutes(start.getMinutes() - 15);
            }
            if (!end || isNaN(end)) {
                end = new Date();
            }
        }
        start = new Date(end.getTime() - 900000); // for demo
        start = dateformat(start,"ddmmyyyyHHMMss");
        end = dateformat(end,"ddmmyyyyHHMMss");
        mmsi = parseInt(params.mmsi, 10); isNaN(mmsi) && (mmsi = undefined);
        typeOfShip = parseInt(params.typeOfShip, 10); isNaN(typeOfShip) && (typeOfShip = undefined);
        return this.client.ApiGet("mmsilist", {mmsi, start, end, typeOfShip}).then(response=> response.data);
    }
    LoadShipPredict(params)
    {
        console.log('dataProvider LoadRoutes begin');
        console.log(params);
        var mmsi, start, end, typeOfShip;
        try {
            start = Extension.ConvertToDate(params.start);
            end = Extension.ConvertToDate(params.end);
        } finally {
            if ((!start || isNaN(start)) && (!end || isNaN(end))) {
                start = new Date();
                start.setMinutes(start.getMinutes() - 15);
            }
            if (!end || isNaN(end)) {
                end = new Date();
            }
        }
        start = new Date(end.getTime() - 900000); // for demo
        start = dateformat(start,"ddmmyyyyHHMMss");
        end = dateformat(end,"ddmmyyyyHHMMss");
        mmsi = parseInt(params.mmsi, 10); isNaN(mmsi) && (mmsi = undefined);
        typeOfShip = parseInt(params.typeOfShip, 10); isNaN(typeOfShip) && (typeOfShip = undefined);
        return this.client.ApiGet("mmsilist", {mmsi, start, end, typeOfShip}).then(response=> response.data);
    }
    LoadConfig(params)
    {
        console.log('dataProvider LoadConfig begin');
        console.log(URLs.URLConfig);
        console.log(this.client);
        return this.client.ApiGet(URLs.URLConfig).then(response => response.data);
    }
    LoadLastRouteDate(params)
    {
        console.log('dataProvider LoadLastFilterDate begin');
        return this.client.ApiGet('lastroutedate').then(response => response.data);
    }
}
export default dataProvider