<template>
    <div class="reg">
        <b-container>
            <b-row>

                <form action="">
                    <div class="modal-card" style="width: 500px" >
                        <header class="modal-card-head">
                            <p class="modal-card-title">Регистрация</p>
                        </header>
                        <section class="modal-card-body">
                            
                            <b-field label="Name">
                                <b-input
                                    type="name"
                                    :value="name"
                                    placeholder="Ваше имя"
                                    required
                                    v-model="name">
                                </b-input>
                            </b-field>
                            
                            <b-field label="Email">
                                <b-input
                                    type="email"
                                    :value="email"
                                    placeholder="Ваш email"
                                    required
                                    v-model="email">
                                </b-input>
                            </b-field>

                            <b-field label="Adress">
                                <b-input
                                    type="adress"
                                    :value="adress"
                                    placeholder="Ваш адрес"
                                    required
                                    v-model="adress">
                                </b-input>
                            </b-field>

                            <b-field label="Password">
                                <b-input
                                    type="password"
                                    :value="password"
                                    password-reveal
                                    placeholder="Ваш пароль"
                                    required
                                    v-model="password">
                                </b-input>
                            </b-field>

                            <b-field label="ConfirmPassword">
                                <b-input
                                    type="password"
                                    :value="confirmPassword"
                                    password-reveal
                                    placeholder="Введите ваш пароль повторно"
                                    required
                                    v-model="confirmPassword">
                                </b-input>
                            </b-field>

                        </section>
                        <footer class="modal-card-foot">
                            <b-button variant="primary" @click="SignOn()">Зарегистрироваться</b-button>
                        </footer>
                    </div>
                </form>

            </b-row>         
        </b-container>               
    </div>
</template>

<script>

import axios from 'axios';
import router from '@/router';

export default {
    data() {
        return {
            name:'',
            email: '',
            password: '',
            adress:'',
            confirmPassword:''
        }
    },
    methods: {
        SignOn() {

                //alert("Регистрация пошла!");
                
                axios.post(this.$ApiUrl + '/Account', {
                    Name: this.name,
                    Email: this.email,
                    Password: this.password,
                    Adress: this.adress,
                    ConfirmPassword: this.confirmPassword
                },
                {
                    headers : {
                        'Content-Type':'application/json',
                        'Accept':'application/json'  
                    }
                },
                {
                    withCredentials: true
                })
                .then(function(response) {
                        const obj = JSON.parse(response.data);
                        localStorage.setItem("Token", obj.Token);
                        localStorage.setItem("Name", obj.Name);
                        localStorage.setItem("Email", obj.Email);
                        localStorage.setItem("RoleId", obj.RoleId);
                        router.push('customer');
                })
                .catch(function (error) {
                        alert(error);
                }); 
                
            }            
    }
}

</script>