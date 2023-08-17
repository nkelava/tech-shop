<template>
  <v-container class="mb-10">
    <v-row class="pt-8 px-4">
      <h1>Dashboard</h1>
    </v-row>
    <v-row>
      <v-col cols="12" sm="4" md="3">
        <slot name="tabs"></slot>
      </v-col>
      <v-col>
        <slot name="tab"></slot>
      </v-col>
    </v-row>
  </v-container>
</template>
