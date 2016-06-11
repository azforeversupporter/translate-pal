import {Aurelia, inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import config from '../config';

@inject(Aurelia, HttpClient)
export class AuthService {

    // As soon as the AuthService is created, we query local storage to
	// see if the login information has been stored. If so, we immediately
    // load it into the session object on the AuthService.
    constructor(aurelia: Aurelia, httpClient: HttpClient) {

        httpClient.configure(http => http.withBaseUrl(config.authUrl));

        this.httpClient = httpClient;
        this.app = aurelia;

        let token = localStorage[config.tokenName];
        if (token) {

            this._session = this.decodeToken(token);

            if (this.isTokenExpired()) {

                // Logout, e.g. clear our token and session.
                this.logout();
            }
        }
    }

    public get session(): any {
        return this._session;
    };
    public get isAuthenticated(): boolean {
        return this.session !== null;
    }

    public hasRole(role: string): boolean {

        let roles: string[] = Array.isArray(this.session.role)
            ? this.session.role
            : [this.session.role];

        return roles.some(r => r === role);
    }

    public login(username: string, password: string): Promise<boolean> {

        return this.httpClient
            .fetch(config.authUrl, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                body: `grant_type=password&username=${username}&password=${password}&scope=roles`
            })
            .then(response => response.json())
            .then((data) => {

                // Save token to localStorage
                localStorage[config.tokenName] = data.access_token;

                // ... and to the session object
                this._session = this.decodeToken(data.access_token);

                return true;
            })
            .catch(reason => false);
    }

    public logout(): void {

        // Clear from localStorage
        localStorage.removeItem(config.tokenName);

        // ... and from the session object
        this._session = null;
    }

    private decodeToken(token: string): any {

        let parts = token.split('.');
        if (parts.length !== 3) {

            throw new Error('JWT must have 3 parts');
        }

        let decoded = this.urlBase64Decode(parts[1]);
        if (!decoded) {

            throw new Error('Cannot decode the token');
        }

        return JSON.parse(decoded);
    }

    private urlBase64Decode(str: string): string {

        let output = str.replace('-', '+').replace('_', '/');
        switch (output.length % 4) {
            case 0: { break; }
            case 2: { output += '=='; break; }
            case 3: { output += '='; break; }
            default: {

                throw new Error('Illegal base64url string!');
            }
        }

        return window.atob(output); // TODO: polifyll https://github.com/davidchambers/Base64.js
    }

    private isTokenExpired(): boolean {

        if (!this.session) {

            return true;
        }

        let d = this.getTokenExpirationDate();
        if (!d) {

            return false;
        }

        // Token expired?
        return true;// !(d.valueOf() > new Date().valueOf());
    }

    private getTokenExpirationDate(): Date {

        if (!this.session) {

            return undefined;
        }

        let d = new Date(0); // The 0 here is the key, which sets the date to the epoch
        d.setUTCSeconds(this.session.exp);

        return d;
    }
    
    private httpClient: HttpClient;
    private app: Aurelia;
    private _session: any = null;
}