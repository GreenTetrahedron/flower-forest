import { Component } from '@angular/core';

@Component({
  selector: 'app-navigation-links',
  templateUrl: './navigation-links.component.html',
  styleUrls: ['./navigation-links.component.css']
})
export class NavigationLinksComponent {
  routes = [
    {routerLink: "/home", name: "home"},
    {routerLink: "/sign-in", name: "sign in"}
  ];

}
