<template>
  <table class="table mb-3">
    <tr>
      <th>Features</th>
      <td>{{ props.score?.features }}</td>
    </tr>
    <tr>
      <th>Comfort</th>
      <td>{{ props.score?.comfort }}</td>
    </tr>
    <tr>
      <th>Quality</th>
      <td>{{ props.score?.quality }}</td>
    </tr>
    <tr>
      <th>Comfort</th>
      <td>{{ props.score?.comfort }}</td>
    </tr>
    <tr>
      <th>Practicality</th>
      <td>{{ props.score?.practicality }}</td>
    </tr>
    <tr>
      <th>Value</th>
      <td>{{ props.score?.value }}</td>
    </tr>
    <tr>
      <th class="has-text-weight-semibold">Total</th>
      <td>{{ props.score?.total }}</td>
    </tr>
  </table>
</template>
<script setup lang="ts">
import { PropType } from "vue";
import { DailyScore } from "../models/dougScore";

const props = defineProps({
  score: {
    type: Object as PropType<DailyScore>,
    required: false,
  },
});
console.log({ ...props.score });
</script>
<style lang="scss" scoped>
@import "bulma/sass/utilities/mixins.sass";
.table {
  width: 75%;
  margin: 0 auto;

  @include mobile {
    width: 100%;
  }
}
</style>
