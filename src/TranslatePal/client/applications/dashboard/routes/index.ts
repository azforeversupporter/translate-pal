import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {Router, RouterConfiguration} from 'aurelia-router';
import 'isomorphic-fetch';

interface Application {
    id?: number;
    defaultLanguage: string;
    displayName: string;
    name: string;
    languages?: string;
    availableLanguages: string[];
}

@inject(HttpClient)
export class Index {

    constructor(http: HttpClient) {

        this.http = http;
        this.http.fetch(`applications`, {
            method: 'GET'
        })
        .then<Application[]>(response => response.json())
        .then(data => this.applications = data)
        .catch(reason => console.log(reason));
    }

    public showDetails(id: number) {

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

    public applications: Application[] = [];
    public router: Router;
    public selectedApp: Application;
    private http: HttpClient = null;
}