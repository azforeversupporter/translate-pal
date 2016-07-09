import {inject, Aurelia} from 'aurelia-framework';
import {AuthService} from 'aurelia-auth';

@inject(AuthService, Aurelia)
export class Navbar {

    constructor(authService: AuthService, aurelia: Aurelia) {

        this.authService = authService;
        this.aurelia = aurelia;
    }

    public logout(evt: Event): void {

        this.authService.logout(null);
        this.aurelia.setRoot('login/login');
    }

    private authService: AuthService;
    private aurelia: Aurelia;
}