export class MeetingDocument {
    id: number;
    documentTitle: string | '';
    documentType: string;
    fileData: File;

    constructor(data: any) {
        this.id = data.id;
        this.documentTitle = data.documentTitle;
        this.documentType = data.documentType;
        this.fileData = data.fileData;
    }
}