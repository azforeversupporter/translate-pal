import {Aurelia} from 'aurelia-framework';
import {bootstrap} from 'aurelia-bootstrapper-webpack';

import authConfig from './config/auth-config';
import {AuthService} from 'aurelia-auth';

import {ConfigBuilder} from 'aurelia-materialize-bridge';

import 'materialize-css/bin/materialize.css';
import 'material-design-icons/iconfont/material-icons.css';

bootstrap(async (aurelia: Aurelia) => {

    aurelia.use
        .standardConfiguration()
        .developmentLogging()
        .plugin('aurelia-auth', (baseConfig) => {
        
            baseConfig.configure(authConfig);
        })
        .plugin('aurelia-materialize-bridge', (config: ConfigBuilder) => config.useAll());

    // Uncomment the line below to enable animation.
    // aurelia.use.plugin('aurelia-animator-css');
    // if the css animator is enabled, add swap-order="after" to all router-view elements.

    // Anyone wanting to use HTMLImports to load views, will need to install the following plugin.
    // aurelia.use.plugin('aurelia-html-import-template-loader');
    const rootElement = document.body;
    rootElement.setAttribute('aurelia-app', '');

    await aurelia.start();

    let auth: AuthService = aurelia.container.get(AuthService);
    let root = auth.isAuthenticated() ? 'dashboard/dashboard' : 'login/login';

    aurelia.setRoot(root, rootElement);

    // if you would like your website to work offline (Service Worker), 
    // install and enable the @easy-webpack/config-offline package in webpack.config.js and uncomment the following code:
    /*
    const offline = await System.import('offline-plugin/runtime');
    offline.install();
    */
});