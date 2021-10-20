import http from "../http_common";

class AuthDataService {

    register(data) {
        return http.post("api/account/register", data, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });
    }
    login(data){
        return http
            .post("api/account/login", data, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });
    }
    getdata(data) {
        return http.get("api/account/getusers", data);        
    }  
    
}

export default new AuthDataService();