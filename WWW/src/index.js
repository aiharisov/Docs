import React from 'react';
import ReactDOM from 'react-dom';
import App from './Components/App/App.js';
import './index.css';
import 'bootstrap/dist/css/bootstrap.css';
import APIClient, * as URLs from './Components/Core/APIClient.js';

const rootElement = document.getElementById('root');
const basePath = rootElement.getAttribute('base-path');
const api = (basePath ? basePath : '') + "/v0/private/";

function renderDOM(settings)
{
    ReactDOM.render(
    <App api-endpoint={api} settings={settings} />,
    rootElement
  );
}

let client = new APIClient({"api-endpoint":api});
client.ApiGet(URLs.URLConfig)
.then((data, props)=>{
  let sett = {};
  for (let row in data.data)
        {
            let curRow = data.data[row];
            sett[curRow["name"]] = curRow["value"];
        }
        renderDOM(sett);

})
.catch(error => {
    console.log(error);
    renderDOM({"api-endpoint":api});
});



