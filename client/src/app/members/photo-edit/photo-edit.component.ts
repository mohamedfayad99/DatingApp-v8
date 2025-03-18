import { Component, inject, input, OnInit, output } from '@angular/core';
import { Member } from '../../_model/member';
import { DecimalPipe, NgClass, NgFor, NgIf, NgStyle } from '@angular/common';
import { FileUploader, FileUploadModule } from 'ng2-file-upload';
import { AccountService } from '../../_sevices/account.service';
import { environment } from '../../../environments/environment';
import { Photo } from '../../_model/photo';
import { MembersService } from '../../_sevices/members.service';

@Component({
  selector: 'app-photo-edit',
  standalone: true,
  imports: [NgIf,NgFor,NgClass,NgStyle,FileUploadModule,DecimalPipe],
  templateUrl: './photo-edit.component.html',
  styleUrl: './photo-edit.component.css'
})
export class PhotoEditComponent implements OnInit {
  private accountservice=inject(AccountService);
  private memberservice=inject(MembersService);
  member=input.required<Member>();
  uploader?:FileUploader;
  hasBaseDropZoneOver=false;
  baseurl="https://localhost:5001/api/";
  memberchange=output<Member>();

  ngOnInit(): void {
  this.initiallizeFileUploader();
  }
  deletephoto(photo:Photo){
    this.memberservice.deletephoto(photo).subscribe({
      next: _ =>{
        const deletephoto={...this.member()};
        deletephoto.photos=deletephoto.photos.filter(x=>x.id !=photo.id);
        this.memberchange.emit(deletephoto);
      }
    })
  }
  setMainPhoto(photo:Photo){
    this.memberservice.setMainPhoto(photo).subscribe({
      next: _ =>{
        const user=this.accountservice.currentuser();
        if(user){
          user.photoUrl=photo.url;
          this.accountservice.SetCurrentUser(user)
        }
        const updatememper={...this.member()}
        updatememper.photoUrl=photo.url;
        updatememper.photos.forEach(p => {
          if(p.isMain) p.isMain=false;
          if(p.id === photo.id)p.isMain=true;
        });
        this.memberchange.emit(updatememper);
      }
    })
  }
  fileOverBase(e:any){
    this.hasBaseDropZoneOver=e;
  }
  initiallizeFileUploader(){
    this.uploader=new FileUploader({
      url:this.baseurl+'users/add-photo',
      authToken:'Bearer '+this.accountservice.currentuser()?.token,
      isHTML5:true,
      allowedFileType:['image'],
      removeAfterUpload:true,
      autoUpload:false,
      maxFileSize:10 * 1024 * 1024,
    });
    this.uploader.onAfterAddingFile=(file)=>{
      file.withCredentials=false;
    }
    this.uploader.onSuccessItem=(item,respone,header,statue)=>{
      const photo=JSON.parse(respone);
      const updateMember={...this.member()};
      updateMember.photos.push(photo);
      this.memberchange.emit(updateMember);
      
    }
  }

}
