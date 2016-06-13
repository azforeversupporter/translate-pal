import {Aurelia} from 'aurelia-framework';
import {bootstrap} from 'aurelia-bootstrapper-webpack';
import config from './authConfig';
import {AuthService} from 'aurelia-auth';
import * as $ from 'jquery';
(<any>window).jQuery = $;
(<any>window).$ = $;

import './assets/styles/material-icons.css';
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';
import '../node_modules/bootstrap-material-design/dist/css/bootstrap-material-design.min.css';
import '../node_modules/bootstrap-material-design/dist/css/ripples.min.css';
import '../node_modules/bootstrap-material-design/dist/js/material.js';
import '../node_modules/bootstrap-material-design/dist/js/ripples.js';

bootstrap((aurelia: Aurelia): void => {

    aurelia
        .use
        .standardConfiguration()
        .developmentLogging()
        .plugin('aurelia-auth', (baseConfig) => {

            baseConfig.configure(config);
        });

    // After starting the aurelia, we can request the AuthService directly
    // from the DI container on the aurelia object. We can then set the 
    // correct root by querying the AuthService's isAuthenticated method.
    aurelia.start().then(() => {

        let auth: AuthService = aurelia.container.get(AuthService);
        let root = auth.isAuthenticated() ? 'app' : 'login';

        aurelia.setRoot(root, document.body);
    });
});