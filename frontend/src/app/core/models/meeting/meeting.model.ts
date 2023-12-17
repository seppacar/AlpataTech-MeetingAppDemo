export class Meeting {
  title: string
  description: string
  startTime: string
  endTime: string

  constructor(data: any) {
    this.title = data.title;
    this.description = data.description;
    this.startTime = data.startTime;
    this.endTime = data.endTime;
  }
}