import { Component } from '@angular/core';
import { PageService } from '../../core/services/page/page.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  constructor(private pageService: PageService){
    this.pageService.setPageInfo("Home", "HOME PAGE")
  }
}
