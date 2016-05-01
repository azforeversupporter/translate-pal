import {Aurelia} from 'aurelia-framework';
import {bootstrap} from 'aurelia-bootstrapper-webpack';

bootstrap((aurelia: Aurelia): void => {

    aurelia
        .use
        .standardConfiguration()
        .developmentLogging();

    aurelia.start()
        .then(() => aurelia.setRoot('app', document.body));
});