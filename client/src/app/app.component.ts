import { Component, OnInit } from '@angular/core';
import { HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Finance App';
  users: any;

  constructor(private http: HttpClient){

  }
  ngOnInit(): void {
  //this.getUsers();
  }

  getUsers()
  {
    this.http.get('http://localhost:2420/api/category').subscribe(response => {
      this.users = response;
    }, error => {
      console.log(error);
    })
  }
}
