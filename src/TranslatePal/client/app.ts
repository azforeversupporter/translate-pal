import {inject, Aurelia} from 'aurelia-framework';
import {Router, RouterConfiguration} from 'aurelia-router';
import {HttpClient} from 'aurelia-fetch-client';
import {FetchConfig, AuthService} from 'aurelia-auth';

@inject(Aurelia, HttpClient, FetchConfig, AuthService)
export class App {

    constructor(aurelia: Aurelia, http: HttpClient, fetchConfig: FetchConfig, auth: AuthService) {

        this.aurelia = aurelia;
        this.auth = auth;
        this.fetchConfig = fetchConfig;

        http.configure(config => {

            // TODO: Load from configuration file
            config
                .withBaseUrl('http://localhost:5000/api/v1/')
                .withDefaults({
                    headers: {
                        'Accept': 'application/json'
                    }
                })
                .rejectErrorResponses();
        });
    }

    activate() {
        this.fetchConfig.configure();
    }

    router: Router;
    aurelia: Aurelia;

    configureRouter(config: RouterConfiguration, router: Router) {
        
        config.title = 'Translate Pal';
        config.map([
            { route: ['', 'dashboard'], name: 'dashboard', moduleId: './applications/dashboard/routes/index', nav: true, title: 'Dashboard' }
        ]);

        this.router = router;
    }

    public logout(evt: Event) {

        this.auth.logout(undefined);
        this.aurelia.setRoot('login');
    }

    private auth: AuthService;
    private fetchConfig: FetchConfig;
}