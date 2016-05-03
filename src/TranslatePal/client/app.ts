import {inject, Aurelia} from 'aurelia-framework';
import {Router, RouterConfiguration} from 'aurelia-router';

@inject(Aurelia)
export class App {

    constructor(aurelia: Aurelia) {

        this.aurelia = aurelia;
    }

    router: Router;
    aurelia: Aurelia;

    configureRouter(config: RouterConfiguration, router: Router) {
        
        config.title = 'Translate Pal';
        config.map([
            { route: ['', 'home'], name: 'home', moduleId: 'home', nav: true, title: 'Home' }
        ]);

        this.router = router;
    }

    public logout(evt: Event) {

        localStorage.removeItem('loggedIn');
        this.aurelia.setRoot('login');
    }
}