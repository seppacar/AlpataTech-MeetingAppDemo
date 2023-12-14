import { Component } from '@angular/core';



@Component({
  selector: 'app-create-meeting',
  templateUrl: './create-meeting.component.html',
  styleUrls: ['./create-meeting.component.scss']
})
export class CreateMeetingComponent {
  selectedFiles: File[] = [];
  selectedParticipants = [];

  // TODO: Get users from API
  // Filter participants if edit meeting

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length) {
      for (let i = 0; i < input.files.length; i++) {
        this.selectedFiles.push(input.files[i]);
      }
    }
  }

  removeFile(file: File): void {
    this.selectedFiles = this.selectedFiles.filter(f => f !== file);
  }

  selectedCars = [3];
  cars = [
      { id: 1, name: 'Volvo' },
      { id: 2, name: 'Saab', disabled: true },
      { id: 3, name: 'Opel' },
      { id: 4, name: 'Audi' },
      { id: 3, name: 'Opel' },
      { id: 4, name: 'Audi' },
      { id: 3, name: 'Opel' },
      { id: 4, name: 'Audi' },
      { id: 3, name: 'Opel' },
      { id: 4, name: 'Audi' },
  ];

  ngOnInit() {

  }

  toggleDisabled() {
      const car: any = this.cars[1];
      car.disabled = !car.disabled;
  }
}
