import {inject, Aurelia} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {Router, RouterConfiguration} from 'aurelia-router';
import {AuthService} from 'aurelia-auth';

interface Application {
    id?: number;
    defaultLanguage: string;
    displayName: string;
    name: string;
    languages?: string;
    availableLanguages: string[];
}

@inject(HttpClient, Aurelia, AuthService)
export class Dashboard {

    constructor(http: HttpClient, aurelia: Aurelia, authService: AuthService) {

        this.aurelia = aurelia;
        this.authService = authService;
        this.http = http;
        this.http.configure(config => {
            config
                .withBaseUrl(`http://localhost:5000/api/v1/`)
                .withDefaults({
                    headers: {
                        'Accept': 'application/json'
                    }
                })
                .rejectErrorResponses();
        });
        this.http.fetch(`applications`, {
            method: 'GET'
        })
        .then<Application[]>(response => response.json())
        .then(data => this.applications = data)
        .catch(reason => console.error(reason));
    }

    public showDetails(id: number): void {

        this.selectedApp = this.applications.find(app => app.id === id);
    }

    public deleteApp(app: Application) {

        if (confirm(`Are you sure you want to delete the application:\n${app.displayName}?\nThis change cannot be reverted!`)) {

            this.http.fetch(`applications/${app.id}`, {
                method: 'DELETE'
            })
                .then(response => {

                    let index = this.applications.findIndex(a => a.id === app.id);
                    if (index > -1) {

                        this.applications.splice(index, 1);
                        this.selectedApp = null;
                    }
                });
        }
    }

    public logout(evt: Event): void {

        this.authService.logout(null);
        this.aurelia.setRoot('login/login');
    }

    public applications: Application[] = [];
    public router: Router;
    public selectedApp: Application;
    private http: HttpClient = null;
    private aurelia: Aurelia;
    private authService: AuthService;
}