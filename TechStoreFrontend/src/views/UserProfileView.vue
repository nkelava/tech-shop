<script setup>
import { ref } from "vue";
import UserProfile from "@/components/UserProfile.vue";
import UserOrders from "@/components/UserOrders.vue";

const tab = ref("details");
</script>

<template>
  <v-container class="pa-15">
    <v-row>
      <v-breadcrumbs :items="['Home', 'My Account']"></v-breadcrumbs>
    </v-row>
    <v-row class="pt-8 px-4">
      <h1>My Account</h1>
    </v-row>
    <v-row>
      <v-col cols="3">
        <v-tabs v-model="tab" direction="vertical">
          <v-tab class="tab" value="details" color="">
            <v-icon icon="mdi-account" class="mr-2"></v-icon>
            My Details
          </v-tab>
          <v-tab class="tab" value="orders">
            <v-icon icon="mdi-cart" class="mr-2"></v-icon>
            My Orders
          </v-tab>
        </v-tabs>
      </v-col>
      <v-col>
        <v-window v-model="tab">
          <v-window-item value="details">
            <user-profile />
          </v-window-item>
          <v-window-item value="orders">
            <user-orders />
          </v-window-item>
        </v-window>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>
.main {
  background: var(--ts-c-primary-soft);
}

.tab {
  background: var(--ts-c-primary-soft);
  margin-bottom: 1rem;
}

.v-slide-group-item--active {
  background: var(--ts-c-primary-mute);
}

.btn {
  background: var(--ts-c-ternary);
  color: var(--ts-c-text-dark);
  font-weight: bold;
}
</style>
