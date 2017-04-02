import axios from 'axios';

class APIClient
{
    constructor(props) {
        console.log('Init APIClient component');
        let apitimeout = 6000000;
        if (props['settings'] != null)
        {
            apitimeout = props['settings']['apiTimeout'];
        }
        this.API = axios.create({
            baseURL: props['api-endpoint'],
            timeout: apitimeout
        });
        console.log('end');
    }

    ApiGet(URL, params) {
        console.log('APIClient get start');
        return this.API.get(URL, { params });
    }

    ApiPost(URL, postData)
    {
        console.log('APIClient post start');
        console.log(URL);
        console.log(postData);
        return this.API.post(URL, postData);
    }
}
export const URLConfig = "settings/";
export const URLHeatMap = "heatmap";
export const URLPaths = "paths";
export const URLShipTypes = "avalibleshiptypes";
export default APIClient;