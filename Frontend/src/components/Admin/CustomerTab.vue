<template>
    <div>      
        <b-tab title="Заказчики" active>
            <b-button variant="primary modal-button" v-b-modal.modal-add-customer>Добавить</b-button>
                <b-table striped hover :items="CustomerItems" :fields="fields">
                    <template #cell(Id)="data">
                        {{ data.item.id }}
                    </template>
                    <template #cell(Имя)="data">
                        {{ data.item.name}}
                    </template>
                    <template #cell(Email)="data">
                        {{ data.item.email}}
                    </template>
                    <template #cell(Пароль)="data">
                        {{ data.item.password}}
                    </template>
                    <template #cell(Адрес)="data">
                        {{ data.item.adress }}
                    </template>
                    <template #cell(Код)="data">
                        {{ data.item.code }}
                    </template>
                    <template #cell(Скидка)="data">
                        {{ data.item.discount + " %"}}
                    </template>
                    <template #cell(Действия)="data">
                        <b-button variant="primary" size="sm" v-b-modal.modal-edit-customer @click="OpenEditCustomerModal(data.item)">Редактировать</b-button>
                        <b-button variant="danger" size="sm" @click="DeleteCustomer(row.item.id)">Удалить</b-button>
                    </template>
                </b-table>
         </b-tab>

         <!--Создание заказчика-->
         <b-modal id="modal-add-customer" title="Добавление заказчика" 
                hide-footer
                has-modal-card
                trap-focus
                :destroy-on-hide="false"
                aria-role="dialog"
                aria-modal>
                <form action="">
                    <div class="modal-card" style="width: 450px">
                        <section class="modal-card-body">
                            <b-field label="Имя">
                                <b-input
                                    type="Name"
                                    :value="name"
                                    password-reveal
                                    placeholder="Имя"
                                    required
                                    v-model="name">
                                </b-input>
                            </b-field>

                            <b-field label="Адрес">
                                <b-input
                                    type="Address"
                                    :value="address"
                                    placeholder="Адрес"
                                    required
                                    v-model="address">
                                </b-input>
                            </b-field>

                            <b-field label="Скидка">
                                <b-input
                                    type="number"
                                    :value="discount"
                                    min=0
                                    max=100
                                    password-reveal
                                    placeholder="Скидка"
                                    required
                                    v-model="discount">
                                </b-input>
                            </b-field>

                            <b-field label="Email">
                                <b-input
                                    type="Email"
                                    :value="name"
                                    password-reveal
                                    placeholder="Email"
                                    required
                                    v-model="email">
                                </b-input>
                            </b-field>

                            <b-field label="Пароль">
                                <b-input
                                    type="password"
                                    :value="password"
                                    password-reveal
                                    placeholder="Пароль"
                                    required
                                    v-model="password">
                                </b-input>
                            </b-field>
                        </section>
                    <footer class="modal-card-foot">
                        <b-button class="mt-3" variant="outline-primary" block @click="AddCustomer">Добавить</b-button>
                    </footer>
                </div>       
                </form>            
        </b-modal>
        <!--Изменение заказчика-->
        <b-modal id="modal-edit-customer" title="Редактирование заказчика" 
                hide-footer
                has-modal-card
                trap-focus
                :destroy-on-hide="false"
                aria-role="dialog"
                aria-modal>
                <form action="">
                    <div class="modal-card" style="width: 450px">
                        <section class="modal-card-body">
                            <b-field label="Имя">
                                <b-input
                                    type="Name"
                                    :value="name"
                                    password-reveal
                                    placeholder="Имя"
                                    required
                                    v-model="name">
                                </b-input>
                            </b-field>

                            <b-field label="Адрес">
                                <b-input
                                    type="Address"
                                    :value="address"
                                    placeholder="Адрес"
                                    required
                                    v-model="address">
                                </b-input>
                            </b-field>

                            <b-field label="Скидка">
                                <b-input
                                    type="number"
                                    min=0
                                    max=100
                                    :value="discount"
                                    password-reveal
                                    placeholder="Скидка"
                                    required
                                    v-model="discount">
                                </b-input>
                            </b-field>

                            <b-field label="Email">
                                <b-input
                                    type="Email"
                                    :value="name"
                                    password-reveal
                                    placeholder="Email"
                                    required
                                    v-model="email">
                                </b-input>
                            </b-field>

                            <b-field label="Пароль">
                                <b-input
                                    type="Password"
                                    :value="password"
                                    password-reveal
                                    placeholder="Пароль"
                                    required
                                    v-model="password">
                                </b-input>
                            </b-field>
                        </section>
                    <footer class="modal-card-foot">
                        <b-button class="mt-3" variant="outline-primary" block @click="EditCustomer">Изменить</b-button>
                    </footer>
                </div>       
                </form>            
        </b-modal>
    </div>
</template>

<script>

import axios from 'axios';

export default {
    
    data() {
        return {
            id:'',
            userId:'',           
            address:'',
            discount: 0,
            code:'',            
            name:'',
            email:'',
            password:'',
            fields:[
                'Id',
                'Имя',
                'Email',
                'Пароль',
                'Адрес',
                'Код',
                'Скидка',
                'Действия'
            ],
            CustomerItems: []
        }
    },
    created() {
        axios.get(this.$ApiUrl + '/Customer',
                {
                    headers : {
                        'Content-Type':'application/json',
                        'Accept':'application/json',
                        'Authorization':'Bearer ' + localStorage.getItem("Token")
                    }
                },
                {
                    withCredentials: true
                })
                .then((response) => {
                    this.CustomerItems = response.data;                                    
                })
                .catch(function (error) {
                        alert('ERROR ' + error);
                });       
    },
    methods: {
    
        async DeleteCustomer(id) {
            await axios.delete(this.$ApiUrl+ '/Customer/' + id,
                {
                    headers : {
                        'Content-Type':'application/json',
                        'Accept':'application/json',
                        'Authorization':'Bearer ' + localStorage.getItem("Token")
                    }
                },
                {
                    withCredentials: true
                })
                .catch(function (error) {
                        alert('ERROR ' + error);
                });
                
                location.reload();
        },

        async AddCustomer() {
            axios.post(this.$ApiUrl + '/Customer', {
                    adress: this.address,
                    discount: Number.parseInt(this.discount),
                    password: this.password,
                    email: this.email,
                    name: this.name
                },
                {
                    headers : {
                        'Content-Type':'application/json',
                        'Accept':'application/json',
                        'Authorization':'Bearer ' + localStorage.getItem("Token")  
                    }
                },
                {
                    withCredentials: true
                })
                .then(function(response) {
                        alert("Заказчик успешно добавлен!");             
                })
                .catch(function (error) {
                        alert(error);
                });
                
            location.reload();
        },

        async EditCustomer() {
            axios.put(this.$ApiUrl + '/Customer', {
                    id: this.id,
                    userId: this.userId,
                    code: this.code,
                    adress: this.address,
                    discount: Number.parseInt(this.discount),
                    password: this.password,
                    email: this.email,
                    name: this.name
                },
                {
                    headers : {
                        'Content-Type':'application/json',
                        'Accept':'application/json',
                        'Authorization':'Bearer ' + localStorage.getItem("Token")  
                    }
                },
                {
                    withCredentials: true
                })
                .then(function(response) {
                        alert("Заказчик успешно изменён!");             
                })
                .catch(function (error) {
                        alert(error);
                });
                
            location.reload();    
        },

        OpenEditCustomerModal(CustomerData) {
            this.id = CustomerData.id;
            this.userId = CustomerData.userId;
            this.code = CustomerData.code;
            this.name = CustomerData.name;
            this.email = CustomerData.email;
            this.address = CustomerData.adress;
            this.password = CustomerData.password;
            this.discount = CustomerData.discount;
        }
    }
}

</script>
