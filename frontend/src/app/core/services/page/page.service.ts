import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PageService {
  private pageNameSubject = new BehaviorSubject<string>('Default'); // Default name
  private pageDescriptionSubject = new BehaviorSubject<string>('Default'); // Default description

  pageName$ = this.pageNameSubject.asObservable();
  pageDescription$ = this.pageDescriptionSubject.asObservable();

  setPageInfo(name: string, description: string): void {
    this.pageNameSubject.next(name);
    this.pageDescriptionSubject.next(description);
  }
}
