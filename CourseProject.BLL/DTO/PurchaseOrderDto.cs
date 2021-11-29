﻿using CourseProject.DAL.Entities;
using CourseProject.Domain;

namespace CourseProject.BLL.DTO {
    public class PurchaseOrderDto : BaseDto {

        public string ClientId { get; set; }

        public ClientDto Client { get; set; }

        public Guid ManagerId { get; set; }

        public ManagerDto Manager { get; set; }

        public PurchaseOrderState State { get; set; }

        public ICollection<EquipmentItemValue> EquipmentItemsValues { get; set; }
    }
}
