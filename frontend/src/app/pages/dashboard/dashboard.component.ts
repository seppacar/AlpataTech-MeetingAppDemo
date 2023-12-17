import { Component } from '@angular/core';
import { PageService } from '../../core/services/page/page.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {

  constructor(private pageService: PageService) {}
  
  ngOnInit() : void {
    this.pageService.setPageInfo('Dashboard', 'Lorem ipsum dolor sit amet')
  }

}
