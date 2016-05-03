import {inject, Aurelia} from 'aurelia-framework';
import {Router, RouterConfiguration} from 'aurelia-router';
import {HttpClient} from 'aurelia-fetch-client';

@inject(Aurelia, HttpClient)
export class App {

    constructor(aurelia: Aurelia, http: HttpClient) {

        this.aurelia = aurelia;
        http.configure(config => {

            // TODO: Load from configuration file
            config
                .withBaseUrl('http://localhost:4999/api/v1/')
                .withDefaults({
                    headers: {
                        'Accept': 'application/json'
                    }
                })
                .rejectErrorResponses();
        });
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