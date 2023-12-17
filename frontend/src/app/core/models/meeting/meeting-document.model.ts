export class MeetingDocument {
    fileName: string;
    fileExtension: string;
    contentType: string;
    fileData: File;

    constructor(fileName: string, fileExtension: string, contentType: string, fileData: File) {
        this.fileName = fileName;
        this.fileExtension = fileExtension;
        this.contentType = contentType;
        this.fileData = fileData;
    }
}