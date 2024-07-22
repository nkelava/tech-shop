<script setup>
import { computed, onMounted, ref, watch } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import { formatDate } from "@/helpers/formatDate.js";
import { getOrderStatus } from "@/helpers/orderStatus.js";
import { OrderStatus } from "@/constants/enums/order";
import { ITEM_DELETE_FAIL, ITEM_DELETE_SUCCESS } from "@/constants/messages/delete";
import { ITEM_UPDATE_FAIL, ITEM_UPDATE_SUCCESS } from "@/constants/messages/update";
import ConfirmationDialog from "@/components/common/ConfirmationDialog.vue";

const orders = ref([]);
const toast = useToast();
const selectedItemId = ref(null);
const showDialog = ref(false);
const searchQuery = ref("");
const pageState = ref({
  currentPage: 1,
  itemsPerPage: 10,
});

onMounted(() => reloadOrders());

async function reloadOrders() {
  const resp = await axiosPrivate.get("/orders/all").catch((error) => console.log(error));

  if (resp.status !== 200) return;

  orders.value = resp.data;
}

const openConfirmationDialog = (id) => {
  selectedItemId.value = id;
  showDialog.value = true;
};

const deleteOrder = async (orderId) => {
  await axiosPrivate
    .delete(`/orders/${orderId}`)
    .then((resp) => {
      if (resp.status === 200) {
        toast.success(ITEM_DELETE_SUCCESS);
      }
    })
    .catch(() => toast.error(ITEM_DELETE_FAIL));
  await reloadOrders();
};

const updateOrderStatus = async (orderId, orderStatusValue) => {
  await axiosPrivate
    .put("/orders/status", {
      orderId,
      orderStatusValue,
    })
    .then((resp) => {
      if (resp.status === 200) {
        toast.success(ITEM_UPDATE_SUCCESS);
      }
    })
    .catch((error) => {
      toast.error(ITEM_UPDATE_FAIL);
      console.log(error);
    });
  await reloadOrders();
};

const filteredOrders = computed(() => {
  return orders.value.filter(
    (order) =>
      order?.email.toLowerCase().includes(searchQuery.value.trim().toLowerCase()) ||
      order?.id?.toString().includes(searchQuery.value.trim())
  );
});

watch(
  () => orders.value,
  () => {
    const totalItems = filteredOrders.value.length;
    const maxPage = Math.ceil(totalItems / pageState.value.itemsPerPage);

    if (pageState.value.currentPage > maxPage) {
      pageState.value.currentPage = maxPage || 1;
    }
  },
  { immediate: true, deep: true }
);

const totalPageCount = computed(() =>
  Math.ceil(filteredOrders.value.length / pageState.value.itemsPerPage)
);

const currentPageItems = computed(() => {
  return filteredOrders.value.slice(
    (pageState.value.currentPage - 1) * pageState.value.itemsPerPage,
    pageState.value.currentPage * pageState.value.itemsPerPage
  );
});
</script>
<template>
  <v-container class="dashboard__container">
    <v-row class="header-search">
      <h2>Order List</h2>
      <v-text-field
        v-model="searchQuery"
        class="header-search__input"
        label="Search..."
        hide-details="true"
        density="compact"
        variant="outlined"
      />
    </v-row>
    <v-row>
      <v-table class="dashboard__table">
        <thead>
          <tr>
            <th class="text-left">Order n.</th>
            <th class="text-left">Email</th>
            <th class="text-left">Date Placed</th>
            <th class="text-left">Status</th>
            <th class="text-left">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(order, id) in currentPageItems" :key="id">
            <td>{{ order?.id }}</td>
            <td :title="order?.email">{{ order?.email }}</td>
            <td>{{ formatDate(order?.createdAt) }}</td>
            <td>{{ getOrderStatus(order?.status) }}</td>
            <td class="table__actions">
              <v-btn
                color="red"
                icon="mdi-delete"
                size="30"
                title="Delete"
                alt="Delete"
                @click="openConfirmationDialog(order?.id)"
              />
              <v-btn
                color="yellow"
                icon="mdi-progress-clock"
                size="30"
                title="Set to In Progress"
                alt="In Progress"
                @click="updateOrderStatus(order?.id, OrderStatus.PENDING)"
              />
              <v-btn
                color="orange"
                icon="mdi-truck"
                size="30"
                title="Set to Shipped"
                alt="Shipped"
                @click="updateOrderStatus(order?.id, OrderStatus.SHIPPED)"
              />
              <v-btn
                color="green"
                icon="mdi-check"
                size="30"
                title="Set to Completed"
                alt="Completed"
                @click="updateOrderStatus(order?.id, OrderStatus.COMPLETED)"
              />
            </td>
          </tr>
          <tr v-if="orders.length < 1">
            <td>Nothing to see here yet</td>
          </tr>
        </tbody>
      </v-table>
      <v-container v-if="orders.length > pageState.itemsPerPage">
        <v-row justify="center">
          <v-col cols="10">
            <v-container class="max-width">
              <v-pagination v-model="pageState.currentPage" class="my-4" :length="totalPageCount" />
            </v-container>
          </v-col>
        </v-row>
      </v-container>
    </v-row>
    <confirmation-dialog
      title="Confirm Deletion"
      content="Are you sure you want to delete this item?"
      confirmText="Delete"
      :showDialog="showDialog"
      :itemId="selectedItemId"
      @update:showDialog="showDialog = $event"
      @confirm="deleteOrder"
    />
  </v-container>
</template>

<style scoped>
.dashboard__table {
  width: 100%;
  margin-top: 1rem;
  color: var(--ts-c-text-light);
  background: transparent;
  border: 1px solid var(--ts-c-primary-dark);
  border-radius: 10px;
}

thead {
  background: var(--ts-c-primary-dark);
}

tr {
  vertical-align: center;
}

th {
  color: var(--ts-c-text-light) !important;
}

td {
  max-width: 500px !important;
  overflow: hidden;
  text-overflow: ellipsis;
}

.table__actions {
  display: flex;
  align-items: center;
  gap: 1rem;
}
</style>
