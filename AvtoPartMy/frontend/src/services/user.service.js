import http from "../http_common";

class UsersService {
    get_list() {
        return http.get("api/Users");
    }
    del_user(data){
        return http
            .post("api/Users/delete", data);
    } 
    edit(data) {
        return http.post("api/Users/edit", data, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });
    }
            
}

export default new UsersService();